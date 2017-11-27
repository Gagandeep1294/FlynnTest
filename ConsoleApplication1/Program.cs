using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // inner join 
            var result = Table_1.GetAllTable_1().Join(Table_2.GetAllTable_2(), e => e.Table_1Id, 
                                                d => d.ID, (table_2, table_1) => new
                                                {
                                                    Talbe_1Name = table_1.Name,
                                                    Table_2Name = table_1.Name
                                                });

            foreach (var table_1 in result)
            {
                Console.WriteLine(table_1.Id + "\t" + Table_2.Name);
            }
            //left join
            var result = from e in Table_1.GetAllTable_1()
                         join d in Table_2.GetAllTable_2()
                         on e.Id equals d.Id into eGroup
                         from d in eGroup.DefaultIfEmpty()
                         select new
                         {
                             Table1Name = e.Name,
                             Table2Name = d == null ? "No Table2" : d.Name
                         };

            foreach (var v in result)
            {
                Console.WriteLine(v.Table1Name + "\t" + v.Table2Name);
            }

            //outer join
            var result = Table_2.GetAllTable_2()
                                        .GroupJoin(Table_1.GetAllTable_1(),
                                        d => d.Id,
                                        e => e.Code,
                                        (table_2, table_1) => new
                                        {
                                            Table1=table_1,
                                            Table2=table_2
                                        });

            foreach (var table2 in result)
            {
                Console.WriteLine(table2.Table_2.Name);
                foreach (var  in Table2.table1)
                {
                    Console.WriteLine(" " + Table_2.Name);
                }
                Console.WriteLine();
            }

        }
    }
}
