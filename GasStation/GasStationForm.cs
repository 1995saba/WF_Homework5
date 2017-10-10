using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public partial class GasStationForm : Form
    {
        private double quantityOfGas;
        private double price;
        private double gasTotalSum;
        private int priceGas80 = 120;
        private int priceGas92 = 150;
        private int priceGas95 = 165;
        private int priceGas98 = 180;
        private int priceDiesel = 160;
        private int foodTotalSum;
        private int hotDogPrice = 200;
        private int burgerPrice = 250;
        private int frenchFriesPrice = 150;
        private int cokePrice = 100;
        private int hotDogQuantity;
        private int burgerQuantity;
        private int frenchFriesQuantity;
        private int cokeQuantity;
        private double totalSum;
        private double totalValue = 0;
        private static Timer timer;

        public GasStationForm()
        {
            InitializeComponent();

            hotDogPriceLabel.Text = hotDogPrice.ToString();
            burgerPriceLabel.Text = burgerPrice.ToString();
            frenchFriesPriceLabel.Text = frenchFriesPrice.ToString();
            cokePriceLabel.Text = cokePrice.ToString();
        }

        private void GasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((sender as ComboBox).SelectedItem as string) == "АИ 80")
            {
                priceValueLabel.Text = priceGas80.ToString();
                price = priceGas80;
            }
            else if (((sender as ComboBox).SelectedItem as string) == "АИ 92")
            {
                priceValueLabel.Text = priceGas92.ToString();
                price = priceGas92;
            }
            else if (((sender as ComboBox).SelectedItem as string) == "АИ 95")
            {
                priceValueLabel.Text = priceGas95.ToString();
                price = priceGas95;
            }
            else if (((sender as ComboBox).SelectedItem as string) == "АИ 98")
            {
                priceValueLabel.Text = priceGas98.ToString();
                price = priceGas98;
            }
            else if (((sender as ComboBox).SelectedItem as string) == "Диз. топливо")
            {
                priceValueLabel.Text = priceDiesel.ToString();
                price = priceDiesel;
            }
        }

        private void QuantityRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                quantityTextBox.Enabled = true;
                amountTextBox.Enabled = false;
                amountTextBox.Text = null;
            }

            else
            {
                amountTextBox.Enabled = true;
                quantityTextBox.Enabled = false;
                quantityTextBox.Text = null;
            }
        }

        private void QuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            bool result = Double.TryParse(quantityTextBox.Text, out quantityOfGas);
            gasTotalSum = quantityOfGas * price;
            gasTotalSumLabel.Text = gasTotalSum.ToString();
        }

        private void AmountTextBox_TextChanged(object sender, EventArgs e)
        {
            bool result = Double.TryParse(amountTextBox.Text, out gasTotalSum);
            gasTotalSumLabel.Text = gasTotalSum.ToString();
        }

        private void HotDogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hotDogCheckBox.Checked == true)
            {
                hotDogQuantityTextBox.Enabled = true;
            }
            else
            {
                hotDogQuantityTextBox.Enabled = false;
                hotDogQuantityTextBox.Text = null;
            }
        }

        private void BurgerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (burgerCheckBox.Checked == true)
            {
                burgerQuantityTextBox.Enabled = true;
            }
            else
            {
                burgerQuantityTextBox.Enabled = false;
                burgerQuantityTextBox.Text = null;
            }
        }

        private void FrenchFriesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (frenchFriesCheckBox.Checked == true)
            {
                frenchFriesQuantityTextBox.Enabled = true;
            }
            else
            {
                frenchFriesQuantityTextBox.Enabled = false;
                frenchFriesQuantityTextBox.Text = null;
            }
        }

        private void CokeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (cokeCheckBox.Checked == true)
            {
                cokeQuantityTextBox.Enabled = true;
            }
            else
            {
                cokeQuantityTextBox.Enabled = false;
                cokeQuantityTextBox.Text = null;
            }
        }

        public int FoodTotalSumCount()
        {
            bool resultHotDog = Int32.TryParse(hotDogQuantityTextBox.Text, out hotDogQuantity);
            bool resultBurger = Int32.TryParse(burgerQuantityTextBox.Text, out burgerQuantity);
            bool resultFrenchFries = Int32.TryParse(frenchFriesQuantityTextBox.Text, out frenchFriesQuantity);
            bool resultCoke = Int32.TryParse(cokeQuantityTextBox.Text, out cokeQuantity);

            foodTotalSum = hotDogQuantity * hotDogPrice + burgerQuantity * burgerPrice +
                frenchFriesQuantity * frenchFriesPrice + cokeQuantity * cokePrice;
            return foodTotalSum;
        }

        private void HotDogQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            foodTotalSumLabel.Text = FoodTotalSumCount().ToString();
        }

        private void BurgerQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            foodTotalSumLabel.Text = FoodTotalSumCount().ToString();
        }

        private void FrenchFriesQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            foodTotalSumLabel.Text = FoodTotalSumCount().ToString();
        }

        private void CokeQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            FoodTotalSumCount();
            foodTotalSumLabel.Text = foodTotalSum.ToString();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            totalSum = foodTotalSum + gasTotalSum;
            totalAmountLabel.Text = totalSum.ToString();

            timer = new Timer();
            timer.Interval = 10000;
            timer.Start();
            timer.Tick += ShowMessage;
        }

        private void ClearForm()
        {
            foreach (Control elems in this.Controls)
            {
                if (elems is TextBox)
                {
                    ((TextBox)elems).Clear();
                    ((TextBox)elems).Enabled=false;
                }
                else if (elems is CheckBox)
                {
                    ((CheckBox)elems).Checked=false;
                }
                else if (elems is RadioButton)
                {
                    ((RadioButton)elems).Checked = false;
                }
            }
        }

        private void ShowMessage(Object myObject, EventArgs myEventArgs)
        {
            timer.Stop();

            DialogResult result = MessageBox.Show(
                "Очистить введенные данные?",
                "Сообщение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                totalValue += totalSum;
                totalValueLabel.Text = totalValue.ToString();
                ClearForm();
            }
        }
    }
}
