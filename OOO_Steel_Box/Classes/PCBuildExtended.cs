using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace OOO_Steel_Box.Classes
{
    public class PCBuildExtended
    {
        public Model.PCBuilds PCBuilds { get; set; }
        public string ProductPathPhoto
        {
            get
            {
                string temp;
                if (!String.IsNullOrEmpty(this.PCBuilds.PCBuildImage))
                {
                    temp = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Resources/") + this.PCBuilds.PCBuildImage;
                }
                else
                {
                    temp = @"\Resources\picture.png";
                }
                return temp;
            }
        }
        //private double productCostWithDiscount;
        public double ProductCostWithDiscount
        {
            get
            {
                double discountAmount = (double)((double)this.PCBuilds.PCBuildPrice * (PCBuilds.PCBuildDiscount / 100.0));
                double priceWithDiscount = (double)PCBuilds.PCBuildPrice - discountAmount;
                return priceWithDiscount;

            }
            set { this.ProductCostWithDiscount = value; }
        }

        public Visibility ProductCostDiscountVisibility
        {
            get
            {
                Visibility result = Visibility.Collapsed;
                if (PCBuilds.PCBuildDiscount > 0)
                {
                    result = Visibility.Visible;
                }
                return result;
            }
        }

        public SolidColorBrush ColorFocused
        {
            get
            {
                SolidColorBrush result = new SolidColorBrush();
                result.Color = Color.FromArgb(255, 255, 255, 255);
                if (PCBuilds.PCBuildDiscount > 15)
                {
                    result.Color = Color.FromArgb(0xFF, 0xCC, 0x66, 0x00);
                }
                return result;
            }
        }
    }
}
