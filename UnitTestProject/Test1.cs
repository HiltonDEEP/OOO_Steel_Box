using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using OOO_Steel_Box.Classes;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace UnitTestProject
{
    [TestClass]
    public class PCBuildExtendedTests
    {
        [TestMethod]
        public void TestProductCostWithDiscountCalculation()
        {
            // Arrange
            var pcBuilds = new OOO_Steel_Box.Model.PCBuilds
            {
                PCBuildPrice = 1000, // Цена без скидки
                PCBuildDiscount = 10 // Скидка в процентах
            };

            var pcBuildExtended = new PCBuildExtended { PCBuilds = pcBuilds };

            // Expected price after discount: 1000 - (1000 * 0.1) = 900
            double expectedPriceWithDiscount = 900;

            // Act
            double actualPriceWithDiscount = pcBuildExtended.ProductCostWithDiscount;

            // Assert
            Assert.AreEqual(expectedPriceWithDiscount, actualPriceWithDiscount);
        }

        [TestMethod]
        public void TestProductPathPhotoAvailability()
        {
            // Arrange
            var pcBuilds = new OOO_Steel_Box.Model.PCBuilds
            {
                PCBuildImage = "example.jpg"
            };
            var pcBuildExtended = new PCBuildExtended { PCBuilds = pcBuilds };

            // Act
            string productPathPhoto = pcBuildExtended.ProductPathPhoto;

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(productPathPhoto));
        }

        [TestMethod]
        public void TestProductCostDiscountVisibility()
        {
            // Arrange
            var pcBuildsNoDiscount = new OOO_Steel_Box.Model.PCBuilds { PCBuildDiscount = 0 };
            var pcBuildExtendedNoDiscount = new PCBuildExtended { PCBuilds = pcBuildsNoDiscount };

            var pcBuildsWithDiscount = new OOO_Steel_Box.Model.PCBuilds { PCBuildDiscount = 10 };
            var pcBuildExtendedWithDiscount = new PCBuildExtended { PCBuilds = pcBuildsWithDiscount };

            // Act
            var visibilityNoDiscount = pcBuildExtendedNoDiscount.ProductCostDiscountVisibility;
            var visibilityWithDiscount = pcBuildExtendedWithDiscount.ProductCostDiscountVisibility;

            // Assert
            Assert.AreEqual(Visibility.Collapsed, visibilityNoDiscount);
            Assert.AreEqual(Visibility.Visible, visibilityWithDiscount);
        }

        [TestMethod]
        public void TestColorFocusedForNoDiscount()
        {
            // Arrange
            var pcBuildsNoDiscount = new OOO_Steel_Box.Model.PCBuilds { PCBuildDiscount = 0 };
            var pcBuildExtendedNoDiscount = new PCBuildExtended { PCBuilds = pcBuildsNoDiscount };

            // Act
            var colorFocused = pcBuildExtendedNoDiscount.ColorFocused;

            // Assert
            Assert.AreEqual(Colors.White, colorFocused.Color);
        }

        [TestMethod]
        public void TestColorFocusedForHighDiscount()
        {
            // Arrange
            var pcBuildsWithHighDiscount = new OOO_Steel_Box.Model.PCBuilds { PCBuildDiscount = 20 };
            var pcBuildExtendedWithHighDiscount = new PCBuildExtended { PCBuilds = pcBuildsWithHighDiscount };

            // Act
            var colorFocused = pcBuildExtendedWithHighDiscount.ColorFocused;

            // Assert
            Assert.AreEqual(Color.FromArgb(0xFF, 0xCC, 0x66, 0x00), colorFocused.Color);
        }




    }
}
