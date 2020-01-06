using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus;
using Janus.Windows.GridEX;

namespace Janus_Test
{
    public partial class Form1 : Form
    {
        int UNIQUEID_index;
        List<int> checkboxes = new List<int>();

        public Form1()
        {
            InitializeComponent();

            SqliteData sqlite = new SqliteData();
            DataTable dtCols = sqlite.selectQuery("SELECT * FROM TbColumns ORDER BY `Order`;");
            DataTable dtRows = sqlite.selectQuery("SELECT * FROM TbBooks;");


            /*string[] columnNames = dtRows.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();*/
            int order = 0;
            foreach (DataRow row in dtCols.Rows)
            {
                string name = row["Name"].ToString();
                string title = row["Title"].ToString();
                string type = row["Type"].ToString();

                if (type == "CheckBox")
                    this.checkboxes.Add(order);

                if (name == "UniqueId")
                    UNIQUEID_index = order;

                try
                {
                    dtRows.Columns[name].SetOrdinal(order++);
                    dtRows.Columns[name].ColumnName = title;
                }
                catch
                {
                    MessageBox.Show(order.ToString());
                }
            }

            this.gridEX1.SetDataBinding(dtRows, "");
            this.gridEX1.RetrieveStructure();
            //this.gridEX1.Columns("");
        }

        private void gridEX1_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {
           
            foreach (int index in this.checkboxes.ToArray())
            {
                int val = Int32.Parse(e.Row.Cells[index].Value.ToString());
                if (val == 0)
                {
                    e.Row.Cells[index].Text = "بله";
                }
                else
                {
                    e.Row.Cells[index].Text = "خیر";
                }
            }
        }

        private void gridEX1_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            MessageBox.Show(e.Row.Cells[5].Value.ToString());
        }
    }
}
