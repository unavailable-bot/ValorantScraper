using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValorantScraper
{
    public partial class ScreenshotsTextBox : Form
    {
        public string ScreenshotsText => txtScreenshots.Text;

        public ScreenshotsTextBox()
        {
            InitializeComponent();
        }

        private void btnCopyTemp_Click(object sender, EventArgs e)
        {
            string screenshotsText = txtScreenshots.Text; // Получаем текст из текстового поля

            // Проверяем, содержит ли текст ссылку "imgur"
            if (!screenshotsText.Contains("imgur"))
            {
                MessageBox.Show("Ссылка должна содержать 'imgur'. Пожалуйста, введите корректную ссылку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Выходим из метода, чтобы не закрывать форму
            }

            // Преобразование ссылки: заменяем точку на пробел между "imgur" и "com"
            screenshotsText = screenshotsText.Replace("imgur.com", "imgur com");
            screenshotsText = screenshotsText.Replace("https://", string.Empty);
            txtScreenshots.Text = screenshotsText; // Обновляем текстовое поле с новой ссылкой

            this.Close();
        }
    }
}
