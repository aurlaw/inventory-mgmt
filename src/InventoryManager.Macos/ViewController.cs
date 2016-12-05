using System;
using System.IO;
using AppKit;
using Foundation;
using InventoryMgr.Shared;

namespace InventoryManager.Macos
{
	public partial class ViewController : NSViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Do any additional setup after loading the view.
			var couchEvents = new CouchbaseEvents();
			byte[] fileBytes = File.ReadAllBytes("gustav.jpeg");
			couchEvents.HelloCBL(fileBytes);

		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}
	}
}
