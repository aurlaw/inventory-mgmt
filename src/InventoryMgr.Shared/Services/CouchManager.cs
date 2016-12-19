//
// CouchManager.cs
//
// Author:
//       Michael Lawrence <mlawrence@aurlaw.com>
//
// Copyright (c) 2016 Michael Lawrence
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using Couchbase.Lite;
using Couchbase.Lite.Auth;


namespace InventoryMgr.Shared.Services
{
	public class CouchManager : IDataManager
	{
		const string DB_NAME = "inventory-mgmt";
		Database db;

		public CouchManager()
		{
			Couchbase.Lite.Storage.SystemSQLite.Plugin.Register();
			db = Manager.SharedInstance.GetDatabase(DB_NAME);
		}

		public string Create(Dictionary<string, object> dataProps)
		{
			var doc = db.CreateDocument();
			var docId = doc.Id;
			doc.PutProperties(dataProps);
			return docId;
		}

		public void Delete(string id)
		{
			var doc = db.GetDocument(id);
			doc.Delete();
		}

		public IDictionary<string, object> GetById(string id)
		{
			var doc = db.GetDocument(id);
			return doc.Properties;
		}

		public void Update(string id, Dictionary<string, object> dataProps)
		{
			var doc = db.GetDocument(id);
			if (doc != null)
			{
				// Save to the Couchbase local Couchbase Lite DB
				doc.PutProperties(dataProps);
			}
		}
	}
}
