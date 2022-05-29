using System;
using System.Activities;
using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace DataTableToHTMLTableConverter

{
    

    public class DataTableToHTMLConverter : CodeActivity

    {

        [Category("Input")]
        [Description("The DataTable object to be converted to HTML")]
        [RequiredArgument]

        public InArgument<DataTable> DataTable { get; set; }

        [Category("Options")]
        [Description("#FFFFFF or White")]
        public InArgument<string> HeaderBackgroundColor { get; set; }

        [Category("Options")]
        [Description("#FFFFFF or White")]
        public InArgument<string> BodyBackgroundColor { get; set; }



        [Category("Options")]
        [Description("\"0\"")]
        public InArgument<string> TableBorder { get; set; }



        [Category("Options")]
        [Description("\"0\"")]
        public InArgument<string> TableCellPadding { get; set; }



        [Category("Options")]
        [Description("\"0\"")]
        public InArgument<string> TableCellSpacing { get; set; }



        [Category("Output")]
    
        public OutArgument<string> HTMLTable { get; set; }





        public string generateHtml(DataTable data_table,string headerBackgroundColor ,string backgroundColor, string border, string spacing, string padding)

        {

            string textBody = " <table border=" + Convert.ToInt32(border) + " cellpadding=" + Convert.ToInt32(padding) + " cellspacing=" + Convert.ToInt32(spacing) + "><tr bgcolor='" + headerBackgroundColor + "'>";

            foreach (DataColumn dataColumn in data_table.Columns)

            {

                textBody += "<td><b>" + dataColumn.ColumnName + "</b></td>";

            }



            textBody += "</tr>";

            for (int loopCount = 0; loopCount < data_table.Rows.Count; loopCount++)

            {

                textBody += "<tr>";

                foreach (DataColumn dataColumn in data_table.Columns)
                {

                    textBody += "<td bgcolor='"+ backgroundColor +"'> " + data_table.Rows[loopCount][dataColumn.ColumnName] + "</td>";

                }

                textBody += "</tr>";



            }

            textBody += "</table>";

            return textBody;

        }



        protected override void Execute(CodeActivityContext context)

        {

            var dataTable = DataTable.Get(context);

            var headerBackgroundColor = HeaderBackgroundColor.Get(context) == null ? "white" : HeaderBackgroundColor.Get(context);
            var backgroundColor = BodyBackgroundColor.Get(context) == null ? "white" : BodyBackgroundColor.Get(context);

            var border = TableBorder.Get(context);

            var spacing = TableCellSpacing.Get(context);

            var padding = TableCellPadding.Get(context);

            var text = generateHtml(dataTable,headerBackgroundColor,backgroundColor, border, spacing, padding);

            HTMLTable.Set(context, text);





        }

    }

}