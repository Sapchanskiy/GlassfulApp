using System;
using System.Windows.Forms;
using GlassfullPlugin.Libary;
using System.Text.RegularExpressions;
using GlassfullPlugin.UI;
using TextBox = System.Windows.Forms.TextBox;
using System.Collections.Generic;


namespace GlassfullPlugin.UI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Добавить XML
        /// </summary>
        private Libary.KompasConnector _connector;

        public MainForm()
        {
            InitializeComponent();
            _connector = new Libary.KompasConnector();
        }

        /// <summary>
        /// Метод отображения предупреждения через MessageBox
        /// </summary>
        /// <param name="message">Строка сообщения</param>
        private void ShowMessage(string message)
        {
            MessageBox.Show(message,
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Валидатор на ввод double 
        /// </summary>
        private void ValidateDoubleTextBoxs_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(), @"[\d\b,]");
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                var parameters = new GlasfullParametrs(double.Parse(WallWidth.Text),
                    double.Parse(HighDiameter.Text),
                    double.Parse(Height.Text),
                    double.Parse(BottomThickness.Text),
                    double.Parse(LowDiameter.Text));
                _connector.OpenKompas();
                var builder = new DetailBuilder(_connector.Kompas);
                builder.CreateDetail(parameters, FacetedGlassCheck.Checked);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Данные введены некоректно \nВозможно есть пустые поля или лишнии запятые",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
