namespace TecnogadgedWin7
{
    public class FilterButtons
    {
        private ComboBox filter;

        public FilterButtons(ComboBox filter)
        {
            this.filter = filter;
        }

        public void SlopeButton_Click(object sender, EventArgs e)
        {
            // Establecer el índice seleccionado del ComboBox a "Pendientes/atrasados"
            filter.SelectedIndex = filter.Items.IndexOf("Pendientes/atrasados");
        }

        public void LaboratoryButton_Click(object sender, EventArgs e)
        {
            filter.SelectedIndex = filter.Items.IndexOf("En laboratorio");
        }

        public void RepairedButton_Click(object sender, EventArgs e)
        {
            filter.SelectedIndex = filter.Items.IndexOf("Reparados");
        }

        public void AllButton_Click(object sender, EventArgs e)
        {
            filter.SelectedIndex = filter.Items.IndexOf("Todos");
        }
    }
}