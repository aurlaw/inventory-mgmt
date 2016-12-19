//
// Test.cs
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
using NUnit.Framework;
using System;
using Autofac;
using InventoryMgr.Shared;
using InventoryMgr.Shared.Services;
using InventoryMgr.Shared.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InventoryMgr.Tests
{
	[TestFixture()]
	public class InventoryModelTest
	{
		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance(new CouchManager()).As<IDataManager>();
			Bootstrap.Container = builder.Build();
		}

		[Test()]
		public void TestToDictionary()
		{
			var now = DateTime.Now;
			var id = Guid.NewGuid().ToString();
			var name = "My Roving";
			decimal price = 12.00m;
			var qty = 1;

			var testRoving = new Roving
			{
				Name = name,
				Price = price,
				ID = id,
				Qty = qty,
				DateCreated = now,
				DateModified = now
			};
			var modelDict = testRoving.ToDictionary();

			var compareDict = new Dictionary<string, object> {
				{ "ID", id},
				{ "Name", name },
				{ "Price", price },
				{ "Qty", qty},
				{ "DateCreated", now },
				{ "DateModified", now },
				{ "InventoryType", InventoryType.ROVING},
				{ "Materials", null},
				{ "Weight", null},
				{ "Length", null}
			};
			CollectionAssert.AreEquivalent(modelDict, compareDict, "not equivalent");
		}
		[Test]
		public void TestDictionaryToModel()
		{
			var now = DateTime.Now;
			var id = Guid.NewGuid().ToString();
			var name = "My Roving";
			decimal price = 12.00m;
			var qty = 1;

			var compareDict = new Dictionary<string, object> {
				{ "ID", id},
				{ "Name", name },
				{ "Price", price },
				{ "Qty", qty},
				{ "DateCreated", now },
				{ "DateModified", now },
				{ "InventoryType", InventoryType.ROVING},
				{ "Materials", null},
				{ "Weight", null},
				{ "Length", null}
			};
			var model = compareDict.FromDictionary<Roving>();
			var testRoving = new Roving
			{
				Name = name,
				Price = price,
				ID = id,
				Qty = qty,
				DateCreated = now,
				DateModified = now
			};

			Assert.AreEqual(JsonConvert.SerializeObject(model), JsonConvert.SerializeObject(testRoving), "Does not contain same props");

		}
	}
}
