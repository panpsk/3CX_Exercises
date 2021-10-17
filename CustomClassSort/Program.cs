using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CustomClassSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.SortMyData();
        }


        public void SortMyData()
        {

            List<MyData> data = FillList();

            //Sort by MyInt
            data = data.OrderBy(x => x.MyInt).ToList();

            //Sort by MyString
            data = data.OrderBy(x => x.MyString).ToList();
        }

        public class MyData
        {
            public int MyInt { get; set; }
            public string MyString { get; set; }
        }

        private List<MyData> FillList()
        {
            //Initialize List of MyData Object
            List<MyData> dummyList = new List<MyData>();
            //const string to be used for random generator
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            //
            for (var i = 1; i <= new Random().Next(); i++)
            {
                MyData pair = new MyData
                {
                    //Generate a random int
                    MyInt = new Random().Next(),
                    //Generate a random string of random length
                    MyString = new string(Enumerable.Repeat(chars, new Random().Next(10,100))
              .Select(s => s[new Random().Next(s.Length)]).ToArray())
                };
                dummyList.Add(pair);
            }
            return dummyList;
        }
    }
}
