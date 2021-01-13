using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pharmacy
{
    /// <summary>
    /// Логика взаимодействия для Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        private int inpCount;
        Medicine tempMed;
        public Dialog(Medicine med)
        {
            InitializeComponent();

            SelectedProd.Text = med.Name_Medicine;
            PriceMed.Text = med.Price_Medicine.ToString();

            tempMed = med;
        }

        private void countBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(countBox.Text, out inpCount);
            decimal result = tempMed.Price_Medicine * inpCount;

            PriceMed.Text = result.ToString();
        }

        private void countBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (inpCount > tempMed.Count_Medicine)
            {
                MessageBox.Show("Exceeded the actual count");
            }
            else
            {
                tempMed.Count_Medicine -= inpCount;

                using (MedicineContext db = new MedicineContext())
                {
                    var tempList = db.Medicines.ToList();

                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (tempList[i].ID_Medicine == tempMed.ID_Medicine)
                        {
                            tempList[i].Count_Medicine = tempMed.Count_Medicine;

                            db.Medicines.Update(tempList[i]);
                            db.SaveChanges();
                        }
                    }
                }
            }

            this.Close();
        }
    }
}
