using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PrinterTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }


        private void Print()
        {

            Console.WriteLine("Impression Start...");

            FlowDocument doc = CreateFlowDocument();
            doc.Name = "FlowDoc";
            // Create IDocumentPaginatorSource from FlowDocument  
            IDocumentPaginatorSource idpSource = doc;
            idpSource.DocumentPaginator.PageSize = new Size(doc.PageWidth, doc.PageHeight);

            Console.WriteLine("Printer SetUp");

            var printDialog = new PrintDialog();

            Console.WriteLine("Printer PrintQueue FullName:" + printDialog.PrintQueue.FullName);
            Console.WriteLine("Printer PrintQueue Description:" + printDialog.PrintQueue.Description);
            Console.WriteLine("Printer PrintQueue IsInError:" + printDialog.PrintQueue.IsInError);

            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

            var capa = printDialog.PrintQueue.GetPrintCapabilities();
            var capabilitiesSizes = capa.PageMediaSizeCapability;
            Console.WriteLine("Sizes");
            foreach (var item in capabilitiesSizes)
            {
                Console.WriteLine("Size (WxH): " + item.Width + " x " + item.Height);

            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("Printer PrintQueue GetPrintCapabilitiesAsXml:" + printDialog.PrintQueue.GetPrintCapabilitiesAsXml().ToString());
            Console.WriteLine("----------------------------");

            Console.WriteLine("Requested Doc Original Size = " + doc.PageWidth + " x " + doc.PageHeight);

            Console.WriteLine("Requested Doc Paginator size = " + idpSource.DocumentPaginator.PageSize.Width + " x " + idpSource.DocumentPaginator.PageSize.Height);

            try
            {
                Console.WriteLine("try");

                if (chrUseDefaultPrinter.IsChecked.Value)
                    printDialog.PrintDocument(idpSource.DocumentPaginator, "Receipt");
                else
                {
                    if (printDialog.ShowDialog() == true)
                    {
                        Console.WriteLine("Selected printer");
                        printDialog.PrintDocument(idpSource.DocumentPaginator, "Receipt");
                    }
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("error:" + e.Message);
            }
            finally
            {
                Console.WriteLine("finally");
            }

            Console.WriteLine("Page sent to the printer Success");

        }

        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument  
            var doc = new FlowDocument();
            doc.PageWidth = 212;
            //doc.MaxPageWidth = 288.0; //An unqualified value is measured in device independent pixels. ... 1in==96px donc 3 pouces max 
            //doc.MaxPageWidth = 300.0; //An unqualified value is measured in device independent pixels. ... 1in==96px donc 3.1 pouces max (80mm)
            doc.MaxPageWidth = 212.0; //An unqualified value is measured in device independent pixels. ... 1in==96px donc 2.2 pouces max (58mm)
            doc.FontFamily = new FontFamily("Courier New");

            // Create a Section  
            var sec = new Section();


            var paragraph = new Paragraph();
            paragraph.FontSize = 12;

            paragraph.Inlines.Add("Merci Thank you");

            sec.Blocks.Add(paragraph);

            // Add Section to FlowDocument  
            doc.Blocks.Add(sec);
            return doc;
        }

    }
}
