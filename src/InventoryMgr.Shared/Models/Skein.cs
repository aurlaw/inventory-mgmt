//
// Skein.cs
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
namespace InventoryMgr.Shared.Models
{
	public class Skein : IInventory
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Qty { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public InventoryType InventoryType
		{
			get { return InventoryType.SKEIN; }
		}

		public string Materials { get; set; }
		public string Weight { get; set; }
		public string Length { get; set; }

		public Skein()
		{
		}
	}
}

/*
 * Define more properties as needed

* Image (attachment) - multiple?

*/
