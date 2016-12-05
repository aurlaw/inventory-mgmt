using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Couchbase.Lite;
using Couchbase.Lite.Auth;

namespace InventoryMgr.Shared
{
	public class CouchbaseEvents
	{
		const string DB_NAME = "couchbaseevents-xam-2";
		Database db;

		public string CreatedDocId { get; private set; }

		public CouchbaseEvents()
		{
			try
			{
				Couchbase.Lite.Storage.SystemSQLite.Plugin.Register();
				db = Manager.SharedInstance.GetDatabase(DB_NAME);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public void HelloCBL(byte[] data = null)
		{
			// Create the document
			this.CreatedDocId = CreateDocument();

			UpdateDocument(CreatedDocId);
			if(data != null)
				AddAttachment(CreatedDocId, data);
		}

		public void Replication()
		{
			var url = new Uri("http://127.0.0.1:5984/couchbaseevents-xam");
			var push = db.CreatePushReplication(url);
			var pull = db.CreatePullReplication(url);
			var auth = AuthenticatorFactory.CreateBasicAuthenticator("xamarain", "xamarain");
			push.Authenticator = auth;
			pull.Authenticator = auth;
			push.Continuous = true;
			pull.Continuous = true;
			push.Changed += (sender, e) =>
			{
				// Will be called when the push replication status changes
				Console.WriteLine(e.Username);
				Console.WriteLine(e.ChangesCount);
				Console.WriteLine(e.Status);
			};
			pull.Changed += (sender, e) =>
			{
				// Will be called when the pull replication status changes
				Console.WriteLine(e.Username);
				Console.WriteLine(e.ChangesCount);
				Console.WriteLine(e.Status);
			};
			pull.Start();
			push.Start();

		}

		public string RetrieveDocument(string docID)
		{
			// retrieve the document from the database
			var retrievedDoc = db.GetDocument(docID);
			// display the retrieved document
			LogDocProperties(retrievedDoc);
			return retrievedDoc.UserProperties["name"].ToString();
		}
		public byte[] ReadAttachment(string docID)
		{
			var doc = db.GetExistingDocument(docID);
			var savedRev = doc.CurrentRevision;
			var attachment = savedRev.GetAttachment("binaryData");
			return attachment.Content.ToArray();
			//using (var sr = new StreamReader(attachment.ContentStream))
			//{
			//	var data = sr.ReadToEnd();
			//	Console.WriteLine("{0}: {1}", TAG, data);
			//}
		}

		static void LogDocProperties(Document doc)
		{
			doc.Properties.Select(x => String.Format("key={0}, value={1}", x.Key, x.Value))
				.ToList().ForEach(y => Console.WriteLine("{0}", y));
		}
		string CreateDocument()
		{
			var doc = db.CreateDocument();
			string docId = doc.Id;
			var props = new Dictionary<string, object> {
				{ "name", "Big Party" },
				{ "location", "MyHouse" },
				{ "date", DateTime.Now }
			};
			doc.PutProperties(props);
			return docId;
		}	
		void UpdateDocument(string docID)
		{
			var doc = db.GetDocument(docID);
			try
			{
				// Update the document with more data
				var updatedProps = new Dictionary<string, object>(doc.Properties);
				updatedProps.Add("eventDescription", "Everyone is invited!");
				updatedProps.Add("address", "123 Elm St.");
				// Save to the Couchbase local Couchbase Lite DB
				doc.PutProperties(updatedProps);
				// display the updated document
				Console.WriteLine("Updated Doc Properties:");
				LogDocProperties(doc);
			}
			catch (CouchbaseLiteException e)
			{
				Console.WriteLine("Error updating properties in Couchbase Lite database: {0}", e);
			}
		}


		void AddAttachment(string docID, byte[] data)
		{
			var doc = db.GetDocument(docID);
			try
			{
				var revision = doc.CurrentRevision.CreateRevision();
				revision.SetAttachment("binaryData", "image/jpeg", data);
				// Save the document and attachment to the local db
				revision.Save();
			}
			catch (CouchbaseLiteException e)
			{
				Console.WriteLine("Error saving attachment: {0}", e);
			}
		}

	}
}
