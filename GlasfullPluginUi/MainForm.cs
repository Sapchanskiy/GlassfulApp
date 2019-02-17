using System;
using System.Windows.Forms;
using GlassfullPlugin.Libary;
using System.Text.RegularExpressions;

namespace GlassfullPlugin.UI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Ссылка на экземпляр, содержащий ссылку на компас. 
        /// </summary>
        private KompasConnector _connector;

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _connector = new KompasConnector();
        }

        /// <summary>
        /// Валидатор на ввод double.
        /// </summary>
        private void ValidateDoubleTextBoxs_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.KeyChar.ToString(), @"[\d\b,]");
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                var parameters = new GlassfulParametrs(double.Parse(WallWidth.Text),
                    double.Parse(HighDiameter.Text),
                    double.Parse(Height.Text),
                    double.Parse(BottomThickness.Text),
                    double.Parse(LowDiameter.Text));
                _connector.OpenKompas();
                var builder = new DetailBuilder(_connector.Kompas);
                builder.CreateDetail(parameters, FacetedGlassCheck.Checked);
            }
            catch (FormatException)
            {
                MessageBox.Show("Данные введены некоректно \nВозможно есть пустые поля или лишние запятые",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
