using System;
using System.Drawing;
using FontAwesome.Sharp;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace TecnogadgedWin7
{
    public partial class ConfirmFinishDelivered : Form
    {
        private Form1 mainForm;
        Button sendButton = new Button();
        Button printButton = new Button();
        int id = 1;
        string name = "";
        string model = "";
        string brand = "";
        string problem = "";
        string diagnostico = "";

        string costo = "";

        int puntoXIcons = 50;
        int puntoXLabels = 100;


        List<string> personalNames = new List<string>();

       public ConfirmFinishDelivered(Form1 form, int id, string name, string tipo_dispositivo, string brand, string model, string status, string problem, string fechaReparado, string costo, string diagnostico, string personaReparo, string personaRecibio, string fechaRecibido, string comment, string refaccion)
        {
            GetPersonalNames();
            string[] fechaHora = fechaRecibido.Split(' ');
            DateTime fecha;
            string fechaFormateada = string.Empty;
            if (DateTime.TryParse(fechaHora[0], out fecha))
            {
                fechaFormateada = fecha.ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
            }

            this.id = id;
            this.costo = costo;
            this.name = name;
            this.model = model;
            this.brand = brand;
            this.problem = problem;
            this.diagnostico = diagnostico;
            InitializeComponent();

            mainForm = form;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(600, 800); // Ajusté el tamaño para acomodar los nuevos campos

            // Crear panel izquierdo
            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.BackColor = Color.Transparent;
            Controls.Add(leftPanel);

            // Titulo del modal
            Label title = new Label();
            title.Text = "Confirmar la entrega del equipo";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.ForeColor = Color.Black;
            title.Location = new Point(100, 20);
            title.Size = new Size(500, 50);
            leftPanel.Controls.Add(title);

            // Subtítulo y valor del nombre
            Label nameSubtitle = new Label();
            nameSubtitle.Text = "Nombre:";
            nameSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            nameSubtitle.ForeColor = Color.Black;
            nameSubtitle.Location = new Point(puntoXLabels, 80);
            nameSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(nameSubtitle);

            Label nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            nameLabel.ForeColor = Color.Black;
            nameLabel.Location = new Point(puntoXLabels, 100);
            nameLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(nameLabel);

            // Icono del nombre
            IconPictureBox nameIcon = new IconPictureBox();
            nameIcon.IconChar = IconChar.User;
            nameIcon.IconColor = Color.FromArgb(31, 30, 68);
            nameIcon.Location = new Point(puntoXIcons, 80);
            nameIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(nameIcon);

            // Subtítulo y valor del tipo de dispositivo
            Label deviceTypeSubtitle = new Label();
            deviceTypeSubtitle.Text = "Tipo de dispositivo:";
            deviceTypeSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            deviceTypeSubtitle.ForeColor = Color.Black;
            deviceTypeSubtitle.Location = new Point(puntoXLabels, 140);
            deviceTypeSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(deviceTypeSubtitle);

            Label deviceTypeLabel = new Label();
            deviceTypeLabel.Text = tipo_dispositivo;
            deviceTypeLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            deviceTypeLabel.ForeColor = Color.Black;
            deviceTypeLabel.Location = new Point(puntoXLabels, 160);
            deviceTypeLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(deviceTypeLabel);

            // Icono del tipo de dispositivo
            IconPictureBox deviceTypeIcon = new IconPictureBox();
            deviceTypeIcon.IconChar = IconChar.MobileAlt;
            deviceTypeIcon.IconColor = Color.FromArgb(31, 30, 68);
            deviceTypeIcon.Location = new Point(puntoXIcons, 140);
            deviceTypeIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(deviceTypeIcon);

            // Subtítulo y valor de la marca
            Label brandSubtitle = new Label();
            brandSubtitle.Text = "Marca:";
            brandSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            brandSubtitle.ForeColor = Color.Black;
            brandSubtitle.Location = new Point(puntoXLabels, 200);
            brandSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(brandSubtitle);

            Label brandLabel = new Label();
            brandLabel.Text = brand;
            brandLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            brandLabel.ForeColor = Color.Black;
            brandLabel.Location = new Point(puntoXLabels, 220);
            brandLabel.Size = new Size(150, 20);
            leftPanel.Controls.Add(brandLabel);

            // Icono de la marca
            IconPictureBox brandIcon = new IconPictureBox();
            brandIcon.IconChar = IconChar.Tag;
            brandIcon.IconColor = Color.FromArgb(31, 30, 68);
            brandIcon.Location = new Point(puntoXIcons, 200);
            brandIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(brandIcon);

            // Subtítulo y valor del modelo
            Label modelSubtitle = new Label();
            modelSubtitle.Text = "Modelo:";
            modelSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            modelSubtitle.ForeColor = Color.Black;
            modelSubtitle.Location = new Point(puntoXLabels, 260);
            modelSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(modelSubtitle);

            Label modelLabel = new Label();
            modelLabel.Text = model;
            modelLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            modelLabel.ForeColor = Color.Black;
            modelLabel.Location = new Point(puntoXLabels, 280);
            modelLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(modelLabel);

            // Icono del modelo
            IconPictureBox modelIcon = new IconPictureBox();
            modelIcon.IconChar = IconChar.Mobile;
            modelIcon.IconColor = Color.FromArgb(31, 30, 68);
            modelIcon.Location = new Point(puntoXIcons, 260);
            modelIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(modelIcon);

            // Subtítulo y valor del problema
            Label problemSubtitle = new Label();
            problemSubtitle.Text = "Problema:";
            problemSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            problemSubtitle.ForeColor = Color.Black;
            problemSubtitle.Location = new Point(puntoXLabels, 320);
            problemSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(problemSubtitle);

            Label problemLabel = new Label();
            problemLabel.Text = problem;
            problemLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            problemLabel.ForeColor = Color.Red;
            problemLabel.Location = new Point(puntoXLabels, 340);
            problemLabel.Size = new Size(200, 60);
            leftPanel.Controls.Add(problemLabel);

            // Icono del problema
            IconPictureBox problemIcon = new IconPictureBox();
            problemIcon.IconChar = IconChar.Bug;
            problemIcon.IconColor = Color.FromArgb(31, 30, 68);
            problemIcon.Location = new Point(puntoXIcons, 320);
            problemIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(problemIcon);

            // Subtítulo y valor del comentario
            Label commentSubtitle = new Label();
            commentSubtitle.Text = "Comentarios:";
            commentSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            commentSubtitle.ForeColor = Color.Black;
            commentSubtitle.Location = new Point(puntoXLabels, 420);
            commentSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(commentSubtitle);

            TextBox commentTextBox = new TextBox();
            commentTextBox.Text = comment;
            commentTextBox.Font = new Font("Arial", 12, FontStyle.Regular);
            commentTextBox.ForeColor = Color.DarkOrange;
            commentTextBox.Location = new Point(puntoXLabels, 440);
            commentTextBox.Size = new Size(400, 60);
            commentTextBox.Multiline = true;
            commentTextBox.ScrollBars = ScrollBars.Vertical;
            commentTextBox.ReadOnly = true;
            commentTextBox.BackColor = Color.White;
            leftPanel.Controls.Add(commentTextBox);

            // Icono del comentario
            IconPictureBox commentIcon = new IconPictureBox();
            commentIcon.IconChar = IconChar.Comment;
            commentIcon.IconColor = Color.FromArgb(31, 30, 68);
            commentIcon.Location = new Point(puntoXIcons, 420);
            commentIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(commentIcon);

            // Subtítulo y valor de la persona que recibió
            Label personaRecibioSubtitle = new Label();
            personaRecibioSubtitle.Text = "Recepcionista:";
            personaRecibioSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            personaRecibioSubtitle.ForeColor = Color.Black;
            personaRecibioSubtitle.Location = new Point(puntoXLabels, 520);
            personaRecibioSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(personaRecibioSubtitle);

            Label personaRecibioLabel = new Label();
            personaRecibioLabel.Text = personaRecibio;
            personaRecibioLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            personaRecibioLabel.ForeColor = Color.Black;
            personaRecibioLabel.Location = new Point(puntoXLabels, 540);
            personaRecibioLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(personaRecibioLabel);

            // Icono de la persona que recibió
            IconPictureBox personaRecibioIcon = new IconPictureBox();
            personaRecibioIcon.IconChar = IconChar.UserAlt;
            personaRecibioIcon.IconColor = Color.FromArgb(31, 30, 68);
            personaRecibioIcon.Location = new Point(puntoXIcons, 520);
            personaRecibioIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(personaRecibioIcon);

            // Subtítulo y valor de la fecha recibido
            Label fechaRecibidoSubtitle = new Label();
            fechaRecibidoSubtitle.Text = "Fecha recibido:";
            fechaRecibidoSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            fechaRecibidoSubtitle.ForeColor = Color.Black;
            fechaRecibidoSubtitle.Location = new Point(puntoXLabels, 580);
            fechaRecibidoSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(fechaRecibidoSubtitle);

            Label fechaRecibidoLabel = new Label();
            fechaRecibidoLabel.Text = fechaFormateada;
            fechaRecibidoLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            fechaRecibidoLabel.ForeColor = Color.Black;
            fechaRecibidoLabel.Location = new Point(puntoXLabels, 600);
            fechaRecibidoLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(fechaRecibidoLabel);

            // Icono de la fecha recibido
            IconPictureBox fechaRecibidoIcon = new IconPictureBox();
            fechaRecibidoIcon.IconChar = IconChar.CalendarCheck;
            fechaRecibidoIcon.IconColor = Color.FromArgb(31, 30, 68);
            fechaRecibidoIcon.Location = new Point(puntoXIcons, 580);
            fechaRecibidoIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(fechaRecibidoIcon);

            // Subtítulo y valor de la hora recibido
            Label horaRecibidoSubtitle = new Label();
            horaRecibidoSubtitle.Text = "Hora recibido:";
            horaRecibidoSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            horaRecibidoSubtitle.ForeColor = Color.Black;
            horaRecibidoSubtitle.Location = new Point(puntoXLabels + 220, 580);
            horaRecibidoSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(horaRecibidoSubtitle);

            Label horaRecibidoLabel = new Label();
            DateTime hora;
            if (DateTime.TryParse(fechaHora[1], out hora))
            {
                string amPm = hora.Hour >= 12 ? "PM" : "AM"; // Determinar "AM" o "PM"
                horaRecibidoLabel.Text = hora.ToString("hh:mm", new System.Globalization.CultureInfo("es-ES")) + " " + amPm; // Formato de 12 horas con AM/PM
            }
            else
            {
                horaRecibidoLabel.Text = fechaHora[1]; // En caso de que no se pueda convertir, usar el valor original
            }
            horaRecibidoLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            horaRecibidoLabel.ForeColor = Color.Black;
            horaRecibidoLabel.Location = new Point(puntoXLabels + 220, 600);
            horaRecibidoLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(horaRecibidoLabel);

            // Icono de la hora recibido
            IconPictureBox horaRecibidoIcon = new IconPictureBox();
            horaRecibidoIcon.IconChar = IconChar.Clock;
            horaRecibidoIcon.IconColor = Color.FromArgb(31, 30, 68);
            horaRecibidoIcon.Location = new Point(puntoXIcons + 220, 580);
            horaRecibidoIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(horaRecibidoIcon);

            // Subtítulo y valor del fecha de reparado
            Label fechaReparadoSubtitle = new Label();
            fechaReparadoSubtitle.Text = "Fecha reparado:";
            fechaReparadoSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            fechaReparadoSubtitle.ForeColor = Color.Black;
            fechaReparadoSubtitle.Location = new Point(puntoXLabels + 220, 80);
            fechaReparadoSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(fechaReparadoSubtitle);

            // Formatear la fecha de reparación
            string[] fechaHoraReparado = fechaReparado.Split(' ');
            DateTime fechaReparadoDate;
            string fechaReparadoFormateada = string.Empty;
            if (DateTime.TryParse(fechaHoraReparado[0], out fechaReparadoDate))
            {
                fechaReparadoFormateada = fechaReparadoDate.ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
            }
            
            // Combinar fecha y hora formateadas
            Label fechaReparadoLabel = new Label();
            fechaReparadoLabel.Text = $"{fechaReparadoFormateada}";
            fechaReparadoLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            fechaReparadoLabel.ForeColor = Color.Black;
            fechaReparadoLabel.Location = new Point(puntoXLabels + 220, 100);
            fechaReparadoLabel.Size = new Size(220, 20);
            leftPanel.Controls.Add(fechaReparadoLabel);

            // Icono de la fecha de reparado
            IconPictureBox fechaReparadoIcon = new IconPictureBox();
            fechaReparadoIcon.IconChar = IconChar.CalendarCheck;
            fechaReparadoIcon.IconColor = Color.FromArgb(31, 30, 68);
            fechaReparadoIcon.Location = new Point(puntoXIcons + 220, 80);
            fechaReparadoIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(fechaReparadoIcon);

            // Subtítulo y valor del diagnostico
            Label diagnosticoSubtitle = new Label();
            diagnosticoSubtitle.Text = "Diagnóstico:";
            diagnosticoSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            diagnosticoSubtitle.ForeColor = Color.Black;
            diagnosticoSubtitle.Location = new Point(puntoXLabels + 220, 140);
            diagnosticoSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(diagnosticoSubtitle);

            TextBox diagnosticoTextBox = new TextBox();
            diagnosticoTextBox.Text = diagnostico;
            diagnosticoTextBox.Font = new Font("Arial", 12, FontStyle.Regular);
            diagnosticoTextBox.ForeColor = Color.Green;
            diagnosticoTextBox.Location = new Point(puntoXLabels + 220, 160);
            diagnosticoTextBox.Size = new Size(200, 60);
            diagnosticoTextBox.Multiline = true;
            diagnosticoTextBox.MaxLength = 500;
            diagnosticoTextBox.ScrollBars = ScrollBars.Vertical;
            diagnosticoTextBox.ReadOnly = true;
            diagnosticoTextBox.BackColor = Color.White;
            leftPanel.Controls.Add(diagnosticoTextBox);

            // Icono del diagnostico
            IconPictureBox diagnosticoIcon = new IconPictureBox();
            diagnosticoIcon.IconChar = IconChar.Bug;
            diagnosticoIcon.IconColor = Color.FromArgb(31, 30, 68);
            diagnosticoIcon.Location = new Point(puntoXIcons + 220, 140);
            diagnosticoIcon.Size = new Size(32, 32);
            leftPanel.Controls.Add(diagnosticoIcon);

            // Subtítulo y valor de la persona que atendió
            Label personaAtendioSubtitle = new Label();
            personaAtendioSubtitle.Text = "Diagnosticador:";
            personaAtendioSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            personaAtendioSubtitle.ForeColor = Color.Black;
            personaAtendioSubtitle.Location = new Point(puntoXLabels + 220, 230);
            personaAtendioSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(personaAtendioSubtitle);

            Label personaAtendioLabel = new Label();
            personaAtendioLabel.Text = personaReparo;
            personaAtendioLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            personaAtendioLabel.ForeColor = Color.Purple;
            personaAtendioLabel.Location = new Point(puntoXLabels + 220, 250);
            personaAtendioLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(personaAtendioLabel);

            // Subtítulo y valor del costo
            Label costoSubtitle = new Label();
            costoSubtitle.Text = "Costo:";
            costoSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            costoSubtitle.ForeColor = Color.Black;
            costoSubtitle.Location = new Point(puntoXLabels + 220, 290);
            costoSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(costoSubtitle);

            Label costoLabel = new Label();
            costoLabel.Text = $"${costo}";
            costoLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            costoLabel.ForeColor = Color.Black;
            costoLabel.Location = new Point(puntoXLabels + 220, 310);
            costoLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(costoLabel);

            // Subtítulo y valor de la refacción
            Label refaccionSubtitle = new Label();
            refaccionSubtitle.Text = "Costo de refacción:";
            refaccionSubtitle.Font = new Font("Arial", 12, FontStyle.Bold);
            refaccionSubtitle.ForeColor = Color.Black;
            refaccionSubtitle.Location = new Point(puntoXLabels + 220, 350);
            refaccionSubtitle.Size = new Size(200, 20);
            leftPanel.Controls.Add(refaccionSubtitle);

            Label refaccionLabel = new Label();
            refaccionLabel.Text = $"${refaccion}";
            refaccionLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            refaccionLabel.ForeColor = Color.Black;
            refaccionLabel.Location = new Point(puntoXLabels + 220, 370);
            refaccionLabel.Size = new Size(200, 20);
            leftPanel.Controls.Add(refaccionLabel);

            // Botón de enviar
            sendButton.Text = "Entregar el equipo";
            sendButton.Font = new Font("Arial", 12, FontStyle.Bold);
            sendButton.ForeColor = Color.White;
            sendButton.BackColor = Color.FromArgb(31, 30, 68);
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Location = new Point(160, 650);
            sendButton.Size = new Size(250, 40);
            sendButton.Click += new EventHandler(PutARegister!);
            leftPanel.Controls.Add(sendButton);

            // Botón de imprimir factura
            printButton.Text = "Imprimir factura";
            printButton.Font = new Font("Arial", 12, FontStyle.Bold);
            printButton.ForeColor = Color.White;
            printButton.BackColor = Color.FromArgb(31, 30, 68);
            printButton.FlatStyle = FlatStyle.Flat;
            printButton.FlatAppearance.BorderSize = 0;
            printButton.Location = new Point(160, 700);
            printButton.Size = new Size(250, 40);
            printButton.Click += new EventHandler(PrintInvoice!);
            leftPanel.Controls.Add(printButton);
        }
        public void GetPersonalNames()
        {
            // Instanciar la clase DbConnect y ejecutar la consulta
            DbConnect dbConnect = new DbConnect();
            string query = "SELECT nombre FROM employees";
            DataTable dataTable = dbConnect.ExecuteQuery(query);

            // Agregar los nombres de los empleados al ComboBox
            foreach (DataRow row in dataTable.Rows)
            {
                string name = row["nombre"].ToString();
                personalNames.Add(name);
            }
        }


        // private (string fecha, string hora) SepararFechaHora(string fechaHora)
        // {
        //     if (DateTime.TryParse(fechaHora, out DateTime dateTime))
        //     {
        //         string fecha = dateTime.ToString("yyyy-MM-dd");
        //         string hora = dateTime.ToString("HH:mm:ss");
        //         return (fecha, hora);
        //     }
        //     return (fechaHora, string.Empty);
        // }
        //Función para ocultar los controles superiores del formulario
        protected override CreateParams CreateParams
        {
            get
            {
                // Oculta los controles superiores del formulario
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80; // WS_EX_TOOLWINDOW
                return cp;
            }
        }
        // //Función para ocultar el formulario al perder el foco
        // protected override void OnDeactivate(EventArgs e)
        // {
        //     // Oculta el formulario al perder el foco
        //     base.OnDeactivate(e);
        //     Hide();
        // }
        // Interceptar el mensaje de Windows para evitar mover el formulario
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HTCAPTION = 0x2;

            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
            {
                return;
            }
            base.WndProc(ref m);
        }
        //Funcion para cerrar el modal 
        private void CloseModal(object sender, EventArgs e)
        {
            Hide();
        }
        //PutARegister, cambiar unicamente el estatus a "EN LABORATORIO"
        //             CREATE TABLE IF NOT EXISTS customers (
        //     id INT AUTO_INCREMENT PRIMARY KEY,
        //     nombre VARCHAR(50),
        //     telefono VARCHAR(20),
        //     tipo_dispositivo VARCHAR(50),
        //     marca VARCHAR(50),
        //     modelo VARCHAR(50),
        //     motivo VARCHAR (100),
        //     diagnostico VARCHAR (100),
        //     estatus VARCHAR(20),
        //     persona_recibio VARCHAR(50),
        //     persona_reparo VARCHAR(50),
        //     fecha_recibido DATE,
        //     fecha_reparado DATE
        // );

        private void PutARegister(object sender, EventArgs e)
        {
            try
            {
                DbConnect dbConnect = new DbConnect();
                // Fecha de ingreso automática
                String date_brought = DateTime.Now.ToString("yyyy-MM-dd");

                // Asegúrate de que 'id' tenga un valor válido
                if (id <= 0)
                {
                    throw new Exception("ID no válido.");
                }

                string query = "UPDATE customers SET estatus = @estatus WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@estatus", "ENTREGADO");
                    cmd.Parameters.AddWithValue("@id", id);

                    dbConnect.OpenConnection();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No se encontró el registro con el ID especificado.");
                    }
                }

                string filterValue = mainForm.GetFilterValue();
                string searchValue = mainForm.GetSearchValue();
                mainForm.GetFilterRegisters(filterValue, searchValue);
                MessageBox.Show("Has entregado el equipo al cliente", "Entrega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseModal(sender, e);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PrintInvoice(object sender, EventArgs e)
        {
            string truncatedDiagnostico = diagnostico.Length > 20 ? $"{diagnostico.Substring(0, 20)}..." : diagnostico;

            string receiptText = "[HEADER]TEKNOGADGED\n";
            receiptText += " \n";
            receiptText += $"[TITLE2]TELEFONÍA Y CÓMPUTO\n";
            receiptText += " \n";
            receiptText += $"[TITLE]TEKNOGADGED\n";
            receiptText += " \n";
            receiptText += "[HEADER]NOTA DE VENTA/SERVICIO\n";
            receiptText += " \n";
            receiptText += $"{"[HEADER]Cantidad",-20}{"[HEADER]Producto",-20}{"[HEADER]Precio",-10}\n";
            receiptText += " \n";
            receiptText += $"{1,-10} {$"{truncatedDiagnostico}",-20} ${costo,-10}\n";
            receiptText += $"[TOTAL]TOTAL:${costo}       .";
            receiptText += " \n";
            receiptText += " \n";
            receiptText += $"{DateTime.Now.ToString("yyyy/MM/dd h:mm tt")}\n";
            receiptText += " \n";
            receiptText += "PAGADO\n";
            receiptText += "\n";
            receiptText += "*********************\n";
            receiptText += "¡GRACIAS POR SU PREFERENCIA!\n";
            receiptText += "Se le sugiere guardar su nota para cualquier situación extraordinaria. Es importante que pida su nota después de su pago y verifique que los importes estén correctos para efectos de garantía, la cual corresponde a 15 días a partir de la fecha de entrega.\n";

            // Imprimir la factura
            PrintReceipt(receiptText);
        }

        private void PrintReceipt(string receiptText)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                float yPos = 10;
                float leftMargin = 10;
                float maxWidth = 270; // Ancho máximo de la impresión en puntos (3.78 pulgadas a 72 DPI)

                // Dividir el texto en líneas que se ajusten al ancho de la página
                string[] lines = receiptText.Split('\n');
                foreach (string line in lines)
                {
                    Font printFont;
                    Brush printBrush = Brushes.Black; // Pincel por defecto
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    // Usar una variable temporal para la línea modificada
                    string tempLine = line;

                    switch (true)
                    {
                        case bool _ when tempLine.StartsWith("[HEADER]"):
                            printFont = new Font("Arial", 11, FontStyle.Bold);
                            tempLine = tempLine.Replace("[HEADER]", "");
                            break;
                        case bool _ when tempLine.StartsWith("[TITLE]"):
                            printFont = new Font("Georgia", 13, FontStyle.Bold);
                            printBrush = Brushes.Gray;
                            tempLine = tempLine.Replace("[TITLE]", "");
                            break;
                        case bool _ when tempLine.StartsWith("[TITLE2]"):
                            printFont = new Font("Arial", 12);
                            tempLine = tempLine.Replace("[TITLE2]", "");
                            break;
                        case bool _ when tempLine.StartsWith("[TOTAL]"):
                            printFont = new Font("Arial", 13, FontStyle.Bold);
                            format.Alignment = StringAlignment.Far;
                            tempLine = tempLine.Replace("[TOTAL]", "");
                            break;
                        default:
                            printFont = new Font("Arial", 10, FontStyle.Regular);
                            break;
                    }

                    SizeF size = e.Graphics.MeasureString(tempLine, printFont, (int)maxWidth, format);
                    e.Graphics.DrawString(tempLine, printFont, Brushes.Black, new RectangleF(leftMargin, yPos, maxWidth, size.Height), format);
                    yPos += size.Height;
                }
            };

            // Configurar el tamaño de página predeterminado (A4)
            PaperSize paperSize = new PaperSize("A4", 827, 1169); // Tamaño A4 en unidades de 0.01 pulgadas
            printDocument.DefaultPageSettings.PaperSize = paperSize;
            printDocument.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10); // Márgenes de 10 unidades

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

    }
}
