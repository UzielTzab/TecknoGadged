using FontAwesome.Sharp;
using System.Drawing;
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using System.Runtime.InteropServices;


namespace TecnogadgedWin7
{
    public partial class Form1 : Form
    {
        FilterButtons filterButtons;

        DataGridViewButtonColumn accionesColumn = new DataGridViewButtonColumn();

        public bool isEmployeeSectionActive = false;
        // Variable para almacenar el tipo de filtro seleccionado
        public string filterType = "Cliente";
        //Datos de usuario iniciado

        public string User { get; set; } = "";
        public string Password { get; set; } = "";

        private Label title = new Label();
        private Label description = new Label();
        private Label descriptionFilter = new Label();

        //Crear un contenedor lateral izquierdo
        private Panel leftPanel = new Panel();
        private IconButton icon0 = new IconButton();
        private IconButton icon1 = new IconButton();
        private IconButton icon2 = new IconButton();
        private IconButton icon3 = new IconButton();
        private IconButton ButtonAddCustomer = new IconButton();

        private Panel rightPanel = new Panel();

        //dataGridView para mostrar los datos de la base de datos
        public DataGridView dataGridView = new DataGridView();
        DataGridView dailyReportTable = new DataGridView();

        public ComboBox filterForEmployeeOrClient = new ComboBox();
        public ComboBox employeeSelector = new ComboBox();
        public ComboBox reportPerWeekSelector = new ComboBox();
        public ComboBox dateOptionSelector = new ComboBox();
        public DateTimePicker dateStartOfRange = new DateTimePicker();
        public DateTimePicker dateFinalOfRange = new DateTimePicker();


        //Selector de filtrado
        public ComboBox filter = new ComboBox();
        //Get del filter
        public string GetFilterValue()
        {
            return filter.Text;
        }
        private Button slopeButton = new Button();
        private Button inLaboratoryButton = new Button();
        private Button repairedButton = new Button();
        private Button AllButton = new Button();

        public IconPictureBox indicator = new IconPictureBox();
        public Label indicatorLabel = new Label();

        //Extraer la resolucion de la pantalla
        private int screenWidth = Screen.PrimaryScreen!.Bounds.Width;
        private int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public TextBox search = new TextBox();
        //get del search
        public string GetSearchValue()
        {
            return search.Text;
        }

        ListBox personalList = new ListBox();
        List<string> personalNames = new List<string>();
        List<string> WeeklyReportsPerEmployee = new List<string>();

        private System.Windows.Forms.Timer timer1;
        public DateTimePicker searchDatePicker = new DateTimePicker();

        public Form1()
        {
            //Imagen del formulario
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "brand", "Tek.ico");
            this.Icon = new System.Drawing.Icon(imagePath);
            InitializeComponent();
            // this.Icon = new System.Drawing.Icon("tecnoPreIcon.ico");
            //Abrir la ventana completa
            WindowState = FormWindowState.Maximized;

            OpenLoginForm();
            // Inicializar el DataGridView
            dataGridView.Location = new Point((int)(Width * 0.32), 200);
            //Establecer el tamaño del dataGridView con respecto a la resolucion de la pantalla
            dataGridView.Size = new Size((int)(Width * 1.2), (int)(Height * 1));
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.BorderStyle = BorderStyle.FixedSingle;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 30, 68);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.White;
            dataGridView.RowTemplate.Height = 50;
            dataGridView.ColumnHeadersHeight = 50;
            dataGridView.ReadOnly = true;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 0, 0);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.DefaultCellStyle.Padding = new Padding(10, 10, 10, 10);

            rightPanel.Controls.Add(dataGridView);

            this.Load += new EventHandler(GetAllRegisters!);


            //----------------------------------------------------sizeBox izquierdo----------------------------------------------
            //Configuracion del sizeBox izquierdo, ocupar el todo el alto de la ventana y el 20% del ancho de la ventana
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Width = (int)(Width * 0.30);

            leftPanel.BackColor = Color.FromArgb(31, 30, 68);
            Controls.Add(leftPanel);

            rightPanel.Dock = DockStyle.Fill;
            rightPanel.BackColor = Color.White;
            Controls.Add(rightPanel);

            icon0.IconChar = IconChar.ChartBar;
            icon0.IconColor = Color.White;
            icon0.Dock = DockStyle.Top;
            icon0.FlatStyle = FlatStyle.Flat;
            icon0.FlatAppearance.BorderSize = 0;
            icon0.Text = "Reportes";
            icon0.TextImageRelation = TextImageRelation.ImageBeforeText;
            icon0.Font = new Font("Arial", 12, FontStyle.Bold);
            icon0.ForeColor = Color.White;
            icon0.Height = 60;
            icon0.Click += new EventHandler(employeeManagmentSection!);
            leftPanel.Controls.Add(icon0);


            icon1.IconChar = IconChar.Cogs;
            icon1.IconColor = Color.White;
            icon1.Dock = DockStyle.Top;
            icon1.FlatStyle = FlatStyle.Flat;
            icon1.FlatAppearance.BorderSize = 0;
            icon1.Text = "Configuración";
            icon1.TextImageRelation = TextImageRelation.ImageBeforeText;
            icon1.Font = new Font("Arial", 12, FontStyle.Bold);
            icon1.ForeColor = Color.White;
            icon1.Height = 60;
            icon1.Click += new EventHandler(ConfigurationSection!);
            leftPanel.Controls.Add(icon1);

            if (User == "Admin")
            {
                leftPanel.Controls.Add(icon0);
                leftPanel.Controls.Add(icon1);
            }
            else
            {
                leftPanel.Controls.Remove(icon0);
                leftPanel.Controls.Remove(icon1);
            }

            icon2.IconChar = IconChar.PeopleGroup;
            icon2.IconColor = Color.White;
            icon2.Dock = DockStyle.Top;
            icon2.FlatStyle = FlatStyle.Flat;
            icon2.FlatAppearance.BorderSize = 0;
            icon2.Text = "Atender";
            icon2.TextImageRelation = TextImageRelation.ImageBeforeText;
            icon2.Font = new Font("Arial", 12, FontStyle.Bold);
            icon2.ForeColor = Color.White;
            icon2.Height = 60;
            icon2.Click += new EventHandler(AtentionSection!);
            leftPanel.Controls.Add(icon2);

            icon3.IconChar = User == "Admin" ? IconChar.UserShield : IconChar.UserFriends;
            icon3.IconColor = Color.White;
            icon3.Dock = DockStyle.Top;
            icon3.FlatStyle = FlatStyle.Flat;
            icon3.FlatAppearance.BorderSize = 0;
            icon3.Text = User == "Admin" ? "Administrador" : "Empleado";
            icon3.TextImageRelation = TextImageRelation.ImageBeforeText;
            icon3.Font = new Font("Arial", 12, FontStyle.Bold);
            icon3.ForeColor = Color.White;
            icon3.Height = 60;
            icon3.Click += new EventHandler(PerfilSection!);
            leftPanel.Controls.Add(icon3);

            Panel sizeBox = new Panel();
            sizeBox.Dock = DockStyle.Top;
            sizeBox.Height = 40;
            sizeBox.BackColor = Color.FromArgb(31, 30, 68);
            leftPanel.Controls.Add(sizeBox);



            ButtonAddCustomer.IconChar = IconChar.UserEdit;
            ButtonAddCustomer.IconColor = Color.White;
            ButtonAddCustomer.FlatStyle = FlatStyle.Flat;
            ButtonAddCustomer.FlatAppearance.BorderSize = 0;
            ButtonAddCustomer.Height = 60;
            ButtonAddCustomer.Width = 200;
            ButtonAddCustomer.Text = "Nuevo cliente";
            ButtonAddCustomer.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonAddCustomer.Font = new Font("Arial", 12, FontStyle.Bold);
            ButtonAddCustomer.ForeColor = Color.White;
            ButtonAddCustomer.BackColor = Color.FromArgb(26, 128, 229);

            // Calcular la posición X del botón
            int icon4X = (int)(leftPanel.Width * 0.08);

            // Establecer la posición Y del botón para que esté cerca de la parte inferior del panel
            int icon4Y = leftPanel.Height - ButtonAddCustomer.Height - 20; // 20 píxeles de margen desde la parte inferior

            ButtonAddCustomer.Location = new Point(icon4X, icon4Y);

            // Anclar el botón a la parte inferior del panel
            ButtonAddCustomer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            ButtonAddCustomer.Click += new EventHandler(OpenCustomerFormButton_Click!);
            leftPanel.Controls.Add(ButtonAddCustomer);


            Panel sizeBox2 = new Panel();
            sizeBox2.Dock = DockStyle.Bottom;
            sizeBox2.Height = 40;
            sizeBox2.BackColor = Color.FromArgb(31, 30, 68);
            leftPanel.Controls.Add(sizeBox2);


            //----------------------------------------------------Cuerpo del panel derecho----------------------------------------------

            //Titulo del negocio
            title.Text = "Clientes";
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            title.Location = new Point(300, 50);
            title.Size = new Size(200, 50);
            rightPanel.Controls.Add(title);

            //Descripcion del negocio
            description.Text = "Filtrar por fecha";
            description.Font = new Font("Arial", 15, FontStyle.Regular);
            description.ForeColor = Color.Gray;
            description.Location = new Point(300, 100);
            description.Size = new Size(150, 50);
            rightPanel.Controls.Add(description);

            // Crear el ComboBox para seleccionar la opción de fecha
            dateOptionSelector.Location = new Point(460, 100);
            dateOptionSelector.ForeColor = Color.White;
            dateOptionSelector.BackColor = Color.FromArgb(31, 30, 68);
            dateOptionSelector.Size = new Size(200, 30);
            dateOptionSelector.FlatStyle = FlatStyle.Flat;
            dateOptionSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            dateOptionSelector.Items.Add("Ninguna fecha");
            dateOptionSelector.Items.Add("Filtrar por fecha de reparación");
            dateOptionSelector.Items.Add("Filtrar por fecha recibido");
            dateOptionSelector.Items.Add("Filtrar por fecha de entrega");
            dateOptionSelector.SelectedIndex = 0; // Establecer "Ninguna fecha" como opción predeterminada
            dateOptionSelector.SelectedIndexChanged += new EventHandler(FilterButton_Click!);
            rightPanel.Controls.Add(dateOptionSelector);

            // Manejar el evento SelectedIndexChanged para mostrar u ocultar el DateTimePicker
            dateOptionSelector.SelectedIndexChanged += (sender, e) =>
            {
                if (dateOptionSelector.SelectedItem.ToString() == "Filtrar por fecha de reparación" || dateOptionSelector.SelectedItem.ToString() == "Filtrar por fecha recibido" || dateOptionSelector.SelectedItem.ToString() == "Filtrar por fecha de entrega")
                {
                    dateStartOfRange.Visible = true;
                    dateFinalOfRange.Visible = true;
                }
                else
                {
                    dateStartOfRange.Visible = false;
                    dateFinalOfRange.Visible = false;
                }
            };

            // DatePicker Inicial
            dateStartOfRange.Location = new Point(460, 120);
            dateStartOfRange.ForeColor = Color.White;
            dateStartOfRange.CalendarForeColor = Color.FromArgb(31, 30, 68);
            dateStartOfRange.Visible = false; // Ocultar el DateTimePicker por defecto
            dateStartOfRange.ValueChanged += new EventHandler(FilterButton_Click!);
            rightPanel.Controls.Add(dateStartOfRange);

            // DatePicker final
            dateFinalOfRange.Location = new Point(460, 140);
            dateFinalOfRange.ForeColor = Color.White;
            dateFinalOfRange.CalendarForeColor = Color.FromArgb(31, 30, 68);
            dateFinalOfRange.Visible = false;
            dateFinalOfRange.ValueChanged += new EventHandler(FilterButton_Click!);
            rightPanel.Controls.Add(dateFinalOfRange);

            //Descripcion del uso del filtrado por estado
            descriptionFilter.Text = "Filtrar por estado";
            descriptionFilter.Font = new Font("Arial", 15, FontStyle.Regular);
            descriptionFilter.ForeColor = Color.Gray;
            descriptionFilter.Location = new Point(700, 80);
            descriptionFilter.Size = new Size(200, 20);
            rightPanel.Controls.Add(descriptionFilter);

            //Icono de busqueda
            IconPictureBox searchIcon = new IconPictureBox();
            searchIcon.IconChar = IconChar.Search;
            searchIcon.IconColor = Color.FromArgb(31, 30, 68);
            searchIcon.Location = new Point(300, 150);
            searchIcon.Size = new Size(32, 32);
            searchIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(searchIcon);

            //Campo de busqueda
            search.Font = new Font("Arial", 12, FontStyle.Regular);
            search.ForeColor = Color.White;
            search.Location = new Point(350, 150);
            search.Size = new Size(200, 50);
            search.BackColor = Color.FromArgb(31, 30, 68);
            search.BorderStyle = BorderStyle.FixedSingle;
            search.Font = new Font("Arial", 12, FontStyle.Regular);
            search.TextAlign = HorizontalAlignment.Center;
            search.KeyDown += new KeyEventHandler(Search_KeyDown!);
            rightPanel.Controls.Add(search);

            //Selector para elegir si el buscador buscara por empleado o cliente
            filterForEmployeeOrClient.ForeColor = Color.White;
            filterForEmployeeOrClient.Location = new Point(550, 150);
            filterForEmployeeOrClient.Size = new Size(135, 60);
            filterForEmployeeOrClient.BackColor = Color.FromArgb(31, 30, 68);
            filterForEmployeeOrClient.FlatStyle = FlatStyle.Flat;
            filterForEmployeeOrClient.DropDownStyle = ComboBoxStyle.DropDownList;
            filterForEmployeeOrClient.Items.Add("Cliente");
            filterForEmployeeOrClient.Items.Add("Empleado que reparó");
            filterForEmployeeOrClient.Items.Add("Marca");
            filterForEmployeeOrClient.Items.Add("Modelo");

            filterForEmployeeOrClient.SelectedIndexChanged += new EventHandler(FilterForEmployeeOrClient!);
            filterForEmployeeOrClient.SelectedIndex = 0;
            rightPanel.Controls.Add(filterForEmployeeOrClient);

            //Selector de filtrado
            filter.ForeColor = Color.White;
            filter.Location = new Point(700, 120);
            filter.Size = new Size(200, 60);
            filter.BackColor = Color.FromArgb(31, 30, 68);
            filter.FlatStyle = FlatStyle.Flat;
            filter.DropDownStyle = ComboBoxStyle.DropDownList;
            filter.Items.Add("Pendientes/atrasados");
            filter.Items.Add("En laboratorio");
            filter.Items.Add("Reparados");
            filter.Items.Add("Entregados");
            filter.Items.Add("No reparados");
            filter.Items.Add("Todos");
            //Cuando el filtro cambie se ejecutara el evento FilterButton_Click
            filter.SelectedIndexChanged += new EventHandler(FilterButton_Click!);
            filter.SelectedIndex = 0;
            rightPanel.Controls.Add(filter);

            filterButtons = new FilterButtons(filter);

            // Boton para filtrar pendientes y atrasados
            slopeButton.Text = "Pendientes/Atrasados";
            slopeButton.Font = new Font("Arial", 12, FontStyle.Regular);
            slopeButton.Location = new Point(700, 150);
            slopeButton.Size = new Size(200, 40);
            slopeButton.BackColor = Color.FromArgb(31, 30, 68);
            slopeButton.ForeColor = Color.White;
            slopeButton.FlatStyle = FlatStyle.Flat;
            slopeButton.FlatAppearance.BorderSize = 0;
            slopeButton.Click += new EventHandler(filterButtons.SlopeButton_Click);
            rightPanel.Controls.Add(slopeButton);

            //Botón para filtrar en laboratorio
            inLaboratoryButton.Text = "En el laboratorio";
            inLaboratoryButton.Font = new Font("Arial", 12, FontStyle.Regular);
            inLaboratoryButton.Location = new Point(910, 150);
            inLaboratoryButton.Size = new Size(200, 40);
            inLaboratoryButton.BackColor = Color.FromArgb(31, 30, 68);
            inLaboratoryButton.ForeColor = Color.White;
            inLaboratoryButton.FlatStyle = FlatStyle.Flat;
            inLaboratoryButton.FlatAppearance.BorderSize = 0;
            inLaboratoryButton.Click += new EventHandler(filterButtons.LaboratoryButton_Click);
            rightPanel.Controls.Add(inLaboratoryButton);

            // Configurar el IconPictureBox
            indicator.IconChar = IconChar.ChainBroken;
            indicator.IconColor = Color.Gray;
            indicator.Size = new Size(32, 32);
            float indicatorX = dataGridView.Location.X + (dataGridView.Width - indicator.Width) / 2.0f;
            float indicatorY = dataGridView.Location.Y + (dataGridView.Height - indicator.Height) / 2.0f;
            indicator.Location = new Point((int)indicatorX, (int)indicatorY); // Centrar el icono horizontalmente y verticalmente en el dataGridView
            rightPanel.Controls.Add(indicator);
            indicator.BringToFront(); // Asegurarse de que se superponga por encima de todos los componentes

            // Configurar el Label
            indicatorLabel.Text = "Aún no hay registros.";
            indicatorLabel.TextAlign = ContentAlignment.MiddleCenter;
            indicatorLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            indicatorLabel.ForeColor = Color.Gray;
            indicatorLabel.AutoSize = true;
            float labelX = dataGridView.Location.X + (dataGridView.Width - indicatorLabel.Width) / 2.1f;
            float labelY = indicator.Bottom + 10;
            indicatorLabel.Location = new Point((int)labelX, (int)labelY); // Centrar el mensaje debajo del icono en el dataGridView
            rightPanel.Controls.Add(indicatorLabel);
            indicatorLabel.BringToFront(); // Asegurarse de que se superponga por encima de todos los componentes

            //Botón para filtrar en Reparados
            repairedButton.Text = "Reparados";
            repairedButton.Font = new Font("Arial", 12, FontStyle.Regular);
            repairedButton.Location = new Point(910, 100);
            repairedButton.Size = new Size(200, 40);
            repairedButton.BackColor = Color.FromArgb(31, 30, 68);
            repairedButton.ForeColor = Color.White;
            repairedButton.FlatStyle = FlatStyle.Flat;
            repairedButton.FlatAppearance.BorderSize = 0;
            repairedButton.Click += new EventHandler(filterButtons.RepairedButton_Click);
            rightPanel.Controls.Add(repairedButton);

            //Botón para filtrar en Todos
            AllButton.Text = "Todos";
            AllButton.Font = new Font("Arial", 12, FontStyle.Regular);
            AllButton.Location = new Point(910, 50);
            AllButton.Size = new Size(200, 40);
            AllButton.BackColor = Color.FromArgb(31, 30, 68);
            AllButton.ForeColor = Color.White;
            AllButton.FlatStyle = FlatStyle.Flat;
            AllButton.FlatAppearance.BorderSize = 0;
            AllButton.Click += new EventHandler(filterButtons.AllButton_Click);
            rightPanel.Controls.Add(AllButton);

            // // Inicializar el Timer
            // timer1 = new System.Windows.Forms.Timer();
            // timer1.Interval = 60000; // 1 minuto en milisegundos
            // timer1.Tick += new EventHandler(timer1_Tick);
            // timer1.Start();
        }

        private void FilterForEmployeeOrClient(object sender, EventArgs e)
        {
            filterType = filterForEmployeeOrClient.SelectedItem.ToString();
        }
        public void GetFilterRegisters(string filterSelect, string search)
        {
            ExecutePeriodicTask();
            DbConnect dbConnect = new DbConnect();
            string query = string.Empty;

            switch (filterSelect)
            {
                case "Todos":
                    query = "SELECT * FROM customers";
                    break;
                case "Pendientes/atrasados":
                    query = @"
                    SELECT *, 
                        TIMESTAMPDIFF(MINUTE, NOW(), fecha_entregar) AS tiempo_restante
                    FROM customers 
                    WHERE estatus IN ('PENDIENTE', 'ATRASADO')";
                    break;

                case "En laboratorio":
                    query = @"
                    SELECT *, 
                        TIMESTAMPDIFF(MINUTE, NOW(), fecha_entregar) AS tiempo_restante
                    FROM customers 
                    WHERE estatus IN ('EN LABORATORIO')";
                    break;

                case "Reparados":
                    query = "SELECT * FROM customers WHERE estatus = 'REPARADO'";
                    break;

                case "Entregados":
                    query = "SELECT * FROM customers WHERE estatus = 'ENTREGADO'";
                    break;

                case "No reparados":
                    query = "SELECT * FROM customers WHERE estatus = 'NO REPARADO'";
                    break;
            }

            if (!string.IsNullOrEmpty(query))
            {
                if (!string.IsNullOrEmpty(search))
                {
                    if (query.Contains("WHERE"))
                    {
                        if (filterType == "Cliente")
                        {
                            query += " AND nombre LIKE @search";
                        }
                        else if (filterType == "Empleado que reparó")
                        {
                            query += " AND persona_reparo LIKE @search";
                        }
                        else if (filterType == "Marca")
                        {
                            query += " AND marca LIKE @search";
                        }
                        else if (filterType == "Modelo")
                        {
                            query += " AND modelo LIKE @search";
                        }
                    }
                    else
                    {
                        if (filterType == "Cliente")
                        {
                            query += " WHERE nombre LIKE @search";
                        }
                        else if (filterType == "Empleado que reparó")
                        {
                            query += " WHERE persona_reparo LIKE @search";
                        }
                        else if (filterType == "Marca")
                        {
                            query += " WHERE marca LIKE @search";
                        }
                        else if (filterType == "Modelo")
                        {
                            query += " WHERE modelo LIKE @search";
                        }
                    }
                }


                // Filtrar por rango de fechas según la opción seleccionada en el ComboBox
                if (dateOptionSelector.SelectedItem.ToString() != "Ninguna fecha")
                {
                    string dateColumn = string.Empty;
                    switch (dateOptionSelector.SelectedItem.ToString())
                    {
                        case "Filtrar por fecha de reparación":
                            dateColumn = "fecha_reparado";
                            break;
                        case "Filtrar por fecha recibido":
                            dateColumn = "fecha_recibido";
                            break;
                        case "Filtrar por fecha de entrega":
                            dateColumn = "fecha_entregar";
                            break;
                    }

                    if (!string.IsNullOrEmpty(dateColumn))
                    {
                        if (query.Contains("WHERE"))
                        {
                            query += $" AND DATE({dateColumn}) BETWEEN @startDate AND @endDate";
                        }
                        else
                        {
                            query += $" WHERE DATE({dateColumn}) BETWEEN @startDate AND @endDate";
                        }
                    }
                }

                // Añadir la cláusula ORDER BY según el filtro seleccionado
                if (filterSelect == "Pendientes/atrasados" || filterSelect == "En laboratorio")
                {
                    query += " ORDER BY tiempo_restante ASC";
                }
                else
                {
                    query += " ORDER BY fecha_recibido";
                }

                // Crear un diccionario de parámetros para la consulta
                var parameters = new Dictionary<string, object>
                {
                    { "@search", "%" + search + "%" },
                    { "@startDate", dateStartOfRange.Value.Date },
                    { "@endDate", dateFinalOfRange.Value.Date }
                };
                DataTable dataTable = dbConnect.ExecuteQuery(query, parameters);

                // Cambiar los nombres de las columnas a nombres más cómodos
                dataTable.Columns["id"]!.ColumnName = "ID";
                dataTable.Columns["nombre"]!.ColumnName = "Nombre del cliente";
                dataTable.Columns["telefono"]!.ColumnName = "Teléfono";
                dataTable.Columns["tipo_dispositivo"]!.ColumnName = "Tipo de Dispositivo";
                dataTable.Columns["marca"]!.ColumnName = "Marca";
                dataTable.Columns["modelo"]!.ColumnName = "Modelo";
                dataTable.Columns["estatus"]!.ColumnName = "Estatus";
                dataTable.Columns["comentarios"]!.ColumnName = "Comentarios"; // Nuevo campo
                dataTable.Columns["fecha_entregar"]!.ColumnName = "Fecha de Entrega";
                dataTable.Columns["motivo"]!.ColumnName = "Motivo";
                dataTable.Columns["persona_recibio"]!.ColumnName = "Persona que Recibió";
                dataTable.Columns["fecha_recibido"]!.ColumnName = "Fecha de Recepción";
                dataTable.Columns["persona_reparo"]!.ColumnName = "Persona que Reparó";
                dataTable.Columns["diagnostico"]!.ColumnName = "Diagnóstico";
                dataTable.Columns["fecha_reparado"]!.ColumnName = "Fecha de Reparación";
                dataTable.Columns["refaccion"]!.ColumnName = "Refacción"; // Nuevo campo
                dataTable.Columns["costo"]!.ColumnName = "Costo";

                // Eliminar la columna 'tiempo_restante' del DataTable
                if (dataTable.Columns.Contains("tiempo_restante"))
                {
                    dataTable.Columns.Remove("tiempo_restante");
                }

                // Asignar los datos al DataGridView
                dataGridView.DataSource = dataTable;
                dataGridView.AllowUserToAddRows = false;

                dataGridView.Columns["ID"].Visible = false;

                // Manejar los eventos
                dataGridView.CellFormatting -= dataGridView_CellFormatting!;
                dataGridView.CellClick -= dataGridView_CellClick!;

                dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting!);
                dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick!);

                // Verificar si la tabla está vacía
                if (dataTable.Rows.Count == 0)
                {
                    // Mostrar el indicador
                    indicator.Visible = true;
                    indicatorLabel.Visible = true;
                }
                else
                {
                    // Ocultar el indicador
                    indicator.Visible = false;
                    indicatorLabel.Visible = false;
                }
            }
        }

        public void GetAllRegisters(object sender, EventArgs e)
        {
            ExecutePeriodicTask();
            DbConnect dbConnect = new DbConnect();
            string query = "SELECT * FROM customers WHERE estatus IN ('PENDIENTE', 'ATRASADO') ORDER BY fecha_recibido DESC";

            // Crear las columnas de botones "Acciones", "Eliminar" y "Editar" antes de asignar los datos
            if (!dataGridView.Columns.Contains("Acciones"))
            {
                // Agregar la columna de botones "Acciones"
                accionesColumn.Name = "Acciones";
                accionesColumn.HeaderCell.Style.ForeColor = Color.FromArgb(255, 204, 204);
                accionesColumn.HeaderText = "Acciones";
                accionesColumn.Text = "Atender";
                accionesColumn.UseColumnTextForButtonValue = true; // Mostrar el texto en los botones    
                accionesColumn.DefaultCellStyle.Padding = new Padding(3, 3, 3, 3);
                dataGridView.Columns.Insert(0, accionesColumn); // Insertar en la primera posición
            }

            if (!dataGridView.Columns.Contains("Eliminar"))
            {
                // Agregar la columna de botones "Eliminar"
                DataGridViewButtonColumn eliminarColumn = new DataGridViewButtonColumn();
                eliminarColumn.Name = "Eliminar";
                eliminarColumn.HeaderCell.Style.ForeColor = Color.FromArgb(255, 204, 204);
                eliminarColumn.HeaderText = "Eliminar";
                eliminarColumn.Text = "Eliminar";
                eliminarColumn.UseColumnTextForButtonValue = true; // Mostrar el texto en los botones    
                dataGridView.Columns.Insert(1, eliminarColumn); // Insertar en la segunda posición
            }
            if (!dataGridView.Columns.Contains("Editar"))
            {
                // Agregar la columna de botones "Editar"
                DataGridViewButtonColumn editarColumn = new DataGridViewButtonColumn();
                editarColumn.Name = "Editar";
                editarColumn.HeaderCell.Style.ForeColor = Color.FromArgb(255, 204, 204);
                editarColumn.HeaderText = "Editar";
                editarColumn.Text = "Editar";
                editarColumn.UseColumnTextForButtonValue = true; // Mostrar el texto en los botones    
                dataGridView.Columns.Insert(2, editarColumn); // Insertar en la tercera posición
            }

            DataTable dataTable = dbConnect.ExecuteQuery(query);

            // Cambiar los nombres de las columnas a nombres más cómodos
            dataTable.Columns["id"]!.ColumnName = "ID";
            dataTable.Columns["nombre"]!.ColumnName = "Nombre del cliente";
            dataTable.Columns["telefono"]!.ColumnName = "Teléfono";
            dataTable.Columns["tipo_dispositivo"]!.ColumnName = "Tipo de Dispositivo";
            dataTable.Columns["marca"]!.ColumnName = "Marca";
            dataTable.Columns["modelo"]!.ColumnName = "Modelo";
            dataTable.Columns["estatus"]!.ColumnName = "Estatus";
            dataTable.Columns["comentarios"]!.ColumnName = "Comentarios"; // Nuevo campo
            dataTable.Columns["fecha_entregar"]!.ColumnName = "Fecha de Entrega";
            dataTable.Columns["motivo"]!.ColumnName = "Motivo";
            dataTable.Columns["persona_recibio"]!.ColumnName = "Persona que Recibió";
            dataTable.Columns["fecha_recibido"]!.ColumnName = "Fecha de Recepción";
            dataTable.Columns["persona_reparo"]!.ColumnName = "Persona que Reparó";
            dataTable.Columns["diagnostico"]!.ColumnName = "Diagnóstico";
            dataTable.Columns["fecha_reparado"]!.ColumnName = "Fecha de Reparación";
            dataTable.Columns["refaccion"]!.ColumnName = "Refacción"; // Nuevo campo
            dataTable.Columns["costo"]!.ColumnName = "Costo";

            // Asignar los datos al DataGridView
            dataGridView.DataSource = dataTable;
            dataGridView.AllowUserToAddRows = false;

            // Manejar los eventos
            dataGridView.CellFormatting -= dataGridView_CellFormatting!;
            dataGridView.CellClick -= dataGridView_CellClick!;

            dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting!);
            dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick!);

            GetFilterRegisters(filter.Text, search.Text);
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic fue en la columna de botones
            if (e.ColumnIndex == dataGridView.Columns["Acciones"].Index && e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                // Obtener el valor de la celda de la fila correspondiente
                var cellValue = dataGridView.Rows[e.RowIndex].Cells["Acciones"].Value;
                // Obtener el valor del estatus
                string statusNow = dataGridView.Rows[e.RowIndex].Cells["Estatus"].Value.ToString()!;

                // Verificar si el estatus es "Pendiente" o "Atrasado"
                if (statusNow == "PENDIENTE" || statusNow == "ATRASADO")
                {
                    int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                    string name = dataGridView.Rows[e.RowIndex].Cells["Nombre del cliente"].Value.ToString()!;
                    string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                    string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                    string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                    string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                    string fechaEntregar = dataGridView.Rows[e.RowIndex].Cells["Fecha de Entrega"].Value.ToString()!;
                    string comentarios = dataGridView.Rows[e.RowIndex].Cells["Comentarios"].Value?.ToString() ?? string.Empty; // Nuevo campo
                                                                                                                               // string refaccion = dataGridView.Rows[e.RowIndex].Cells["Refacción"].Value.ToString()!;

                    // Verificar si el modal ya está abierto
                    if (Application.OpenForms["DeliveredModal"] == null)
                    {
                        OpenDeliveredToLaboratoryModalButton_Click(sender, e, id, name, brand, model, statusNow, motivo, tipoDispositivo, fechaEntregar, comentarios);
                    }
                }
                else if (statusNow == "EN LABORATORIO")
                {
                    int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                    string name = dataGridView.Rows[e.RowIndex].Cells["Nombre del cliente"].Value.ToString()!;
                    string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                    string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                    string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                    string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                    string fechaEntregar = dataGridView.Rows[e.RowIndex].Cells["Fecha de Entrega"].Value.ToString()!;
                    string comentarios = dataGridView.Rows[e.RowIndex].Cells["Comentarios"].Value?.ToString() ?? string.Empty;
                    string refaccion = dataGridView.Rows[e.RowIndex].Cells["Refacción"].Value.ToString()!;

                    OpenRepairFormButton_Click(sender, e, id, name, tipoDispositivo, brand, model, motivo, statusNow, fechaEntregar, comentarios);
                }
                else if (statusNow == "REPARADO" || statusNow == "NO REPARADO")
                {
                    int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                    string name = dataGridView.Rows[e.RowIndex].Cells["Nombre del cliente"].Value.ToString()!;
                    string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                    string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                    string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                    string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                    string fechaReparado = dataGridView.Rows[e.RowIndex].Cells["Fecha de Reparación"].Value.ToString()!;
                    string costo = dataGridView.Rows[e.RowIndex].Cells["Costo"].Value.ToString()!;
                    string diagnostico = dataGridView.Rows[e.RowIndex].Cells["Diagnóstico"].Value.ToString()!;
                    string personaReparo = dataGridView.Rows[e.RowIndex].Cells["Persona que Reparó"].Value.ToString()!;
                    string personaRecibio = dataGridView.Rows[e.RowIndex].Cells["Persona que Recibió"].Value.ToString()!;
                    string fechaRecibido = dataGridView.Rows[e.RowIndex].Cells["Fecha de Recepción"].Value.ToString()!;
                    string comentarios = dataGridView.Rows[e.RowIndex].Cells["Comentarios"].Value?.ToString() ?? string.Empty;
                    string refaccion = dataGridView.Rows[e.RowIndex].Cells["Refacción"].Value.ToString()!;

                    OpenConfirmDeliverdFinished_Click(sender, e, id, name, tipoDispositivo, brand, model, motivo, statusNow, fechaReparado, costo, diagnostico, personaReparo, personaRecibio, fechaRecibido, comentarios, refaccion);
                }
                else if (statusNow == "ENTREGADO")
                {
                    int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                    ConfirmFinished(id);
                }
            }

            // Verificar si el clic fue en la columna de eliminar
            if (e.ColumnIndex == dataGridView.Columns["Eliminar"].Index && e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                DeleteRecordById(id);
            }

            // Verificar si el clic fue en la columna de editar
            if (e.ColumnIndex == dataGridView.Columns["Editar"].Index && e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                string phone = dataGridView.Rows[e.RowIndex].Cells["Teléfono"].Value.ToString()!;
                string name = dataGridView.Rows[e.RowIndex].Cells["Nombre del cliente"].Value.ToString()!;
                string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                string fechaEntregar = dataGridView.Rows[e.RowIndex].Cells["Fecha de Entrega"].Value.ToString()!;
                string statusNow = dataGridView.Rows[e.RowIndex].Cells["Estatus"].Value.ToString()!;
                string comentarios = dataGridView.Rows[e.RowIndex].Cells["Comentarios"].Value?.ToString() ?? string.Empty; // Nuevo campo
                string refaccion = dataGridView.Rows[e.RowIndex].Cells["Refacción"].Value.ToString()!;

                // Verificar si el estado es diferente de "PENDIENTE", "ATRASADO", "EN LABORATORIO" y "REPARADO"
                if (statusNow != "PENDIENTE" && statusNow != "ATRASADO" && statusNow != "EN LABORATORIO" && statusNow != "REPARADO")
                {
                    MessageBox.Show("No puedes editar un registro que ya ha finalizado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Separar la fecha de entrega en fecha y hora
                string[] fechaEntregarArray = fechaEntregar.Split(' ');
                string fecha = fechaEntregarArray[0];
                string hora = fechaEntregarArray[1];

                OpenEditCustomerModal(name, phone, brand, model, motivo, fecha, hora, statusNow, comentarios);
            }
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Costo" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal costo))
                {
                    e.Value = $"${costo:N0}"; // Formatear con signo $ y comas para miles
                    e.FormattingApplied = true;
                }
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Estatus")
            {
                if (e.Value != null)
                {
                    string estatus = e.Value.ToString()!;
                    switch (estatus)
                    {
                        case "PENDIENTE":
                            e.CellStyle!.ForeColor = Color.Orange;
                            break;
                        case "ATRASADO":
                            e.CellStyle!.ForeColor = Color.Red;
                            break;
                        case "REPARADO":
                            e.CellStyle!.ForeColor = Color.Green;
                            break;
                        case "ENTREGADO":
                            e.CellStyle!.ForeColor = Color.DarkCyan;
                            break;
                        case "NO REPARADO":
                            e.CellStyle!.ForeColor = Color.DarkViolet;
                            break;
                        case "EN LABORATORIO":
                            e.CellStyle!.ForeColor = Color.DarkBlue;
                            break;
                        default:
                            e.CellStyle!.ForeColor = Color.Black;
                            break;
                    }
                }
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Entrega")
            {
                if (e.Value != null)
                {
                    string estatus = dataGridView.Rows[e.RowIndex].Cells["Estatus"].Value?.ToString()!;
                    if (estatus == "PENDIENTE")
                    {
                        e.CellStyle!.ForeColor = Color.Orange;
                    }
                    else if (estatus == "ATRASADO")
                    {
                        e.CellStyle!.ForeColor = Color.Red;
                    }
                    else if (estatus == "REPARADO")
                    {
                        e.CellStyle!.ForeColor = Color.Green;
                    }
                    else if (estatus == "ENTREGADO")
                    {
                        e.CellStyle!.ForeColor = Color.DarkCyan;
                    }
                    else if (estatus == "NO REPARADO")
                    {
                        e.CellStyle!.ForeColor = Color.DarkRed;
                    }
                    else if (estatus == "EN LABORATORIO")
                    {
                        e.CellStyle!.ForeColor = Color.DarkBlue;
                    }
                }
            }
            else if (dataGridView.Columns[e.ColumnIndex].Name == "Acciones")
            {
                // Obtener el valor de la celda en la columna "estatus"
                string estatus = dataGridView.Rows[e.RowIndex].Cells["Estatus"].Value?.ToString()!;

                // Cambiar el texto del botón según el valor de "estatus"
                if (estatus == "PENDIENTE")
                {
                    e.Value = "Atender";
                }
                else
                {
                    e.Value = "Entregar";
                }

                // Establecer el estilo del botón según el estatus
                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                buttonCell.FlatStyle = FlatStyle.Flat;
                buttonCell.Style.ForeColor = Color.White;

                switch (estatus)
                {
                    case "PENDIENTE":
                        e.Value = "Atender";
                        buttonCell.Style.BackColor = Color.Orange;
                        break;
                    case "ATRASADO":
                        e.Value = "Atender";
                        buttonCell.Style.BackColor = Color.DarkRed;
                        break;
                    case "REPARADO":
                        e.Value = "Entregar";
                        buttonCell.Style.BackColor = Color.Green;
                        break;
                    case "ENTREGADO":
                        e.Value = "Finalizado";
                        buttonCell.Style.BackColor = Color.DarkCyan;
                        break;
                    case "NO REPARADO":
                        e.Value = "Entregar";
                        buttonCell.Style.BackColor = Color.DarkViolet;
                        break;
                    case "EN LABORATORIO":
                        e.Value = "Reparar";
                        buttonCell.Style.BackColor = Color.DarkBlue;
                        break;
                }
            }
            else if (dataGridView.Columns[e.ColumnIndex].Name == "Editar")
            {
                // Obtener el valor de la celda en la columna "estatus"
                string estatus = dataGridView.Rows[e.RowIndex].Cells["Estatus"].Value?.ToString()!;

                // Deshabilitar el botón de edición si el estatus no es "PENDIENTE" o "ATRASADO"
                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                buttonCell.FlatStyle = FlatStyle.Flat;
                buttonCell.Style.ForeColor = Color.White;

                if (estatus != "PENDIENTE" && estatus != "ATRASADO" && estatus != "EN LABORATORIO" && estatus != "REPARADO")
                {
                    buttonCell.Style.BackColor = Color.Gray;
                    buttonCell.ReadOnly = true;
                }
                else
                {
                    buttonCell.Style.BackColor = Color.FromArgb(31, 30, 68);
                    buttonCell.ReadOnly = false;
                }
            }
            else if (dataGridView.Columns[e.ColumnIndex].Name == "Eliminar")
            {


                // Deshabilitar el botón de edición si el estatus no es "PENDIENTE" o "ATRASADO"
                DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                buttonCell.FlatStyle = FlatStyle.Flat;
                buttonCell.Style.ForeColor = Color.White;

                buttonCell.Style.BackColor = Color.DarkRed;
                buttonCell.ReadOnly = false;

            }
            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Entrega")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fechaEntrega))
                {
                    string amPm = fechaEntrega.Hour >= 12 ? "PM" : "AM"; // Determinar "AM" o "PM"
                    e.Value = fechaEntrega.ToString("dd 'de' MMMM 'de' yyyy 'a las:' hh:mm", new System.Globalization.CultureInfo("es-ES")) + " " + amPm; // Formato con "AM/PM"
                    e.FormattingApplied = true;
                }
                e.CellStyle.BackColor = Color.LightGoldenrodYellow;
                e.CellStyle.ForeColor = Color.Black;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Recepción" || dataGridView.Columns[e.ColumnIndex].Name == "Persona que Recibió")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fechaRecibido))
                {
                    string amPm = fechaRecibido.Hour >= 12 ? "PM" : "AM"; // Determinar "AM" o "PM"
                    e.Value = fechaRecibido.ToString("dd 'de' MMMM 'de' yyyy hh:mm", new System.Globalization.CultureInfo("es-ES")) + " " + amPm; // Formato con "AM/PM"
                    e.FormattingApplied = true;
                }
                e.CellStyle.BackColor = Color.LightSteelBlue;
                e.CellStyle.ForeColor = Color.Black;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Reparación" || dataGridView.Columns[e.ColumnIndex].Name == "Persona que Reparó" || dataGridView.Columns[e.ColumnIndex].Name == "Diagnóstico" || dataGridView.Columns[e.ColumnIndex].Name == "Costo")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fechaReparado))
                {
                    string amPm = fechaReparado.Hour >= 12 ? "PM" : "AM"; // Determinar "AM" o "PM"
                    e.Value = fechaReparado.ToString("dd 'de' MMMM 'de' yyyy hh:mm", new System.Globalization.CultureInfo("es-ES")) + " " + amPm; // Formato con "AM/PM"
                    e.FormattingApplied = true;
                }
                e.CellStyle.BackColor = Color.DarkKhaki;
                e.CellStyle.ForeColor = Color.Black;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Nombre del cliente" || dataGridView.Columns[e.ColumnIndex].Name == "Teléfono")
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.Black;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Tipo de Dispositivo" || dataGridView.Columns[e.ColumnIndex].Name == "Marca" || dataGridView.Columns[e.ColumnIndex].Name == "Modelo")
            {
                e.CellStyle.BackColor = Color.DarkOliveGreen;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Motivo")
            {
                e.CellStyle.BackColor = Color.DarkRed;
                e.CellStyle.ForeColor = Color.White;
            }
            if (dataGridView.Columns[e.ColumnIndex].Name == "Comentarios")
            {
                e.CellStyle.BackColor = Color.LightSalmon;
                e.CellStyle.ForeColor = Color.Black;
            }
            if (dataGridView.Columns[e.ColumnIndex].Name == "Refacción" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal refaccion))
                {
                    e.Value = $"${refaccion:N0}"; // Formatear con signo $ y comas para miles
                    e.FormattingApplied = true;
                }
            }
        }
        //Sección de reportes, contiene dos tablas, una para los empleados y otra para los reportes semanales
        private DataGridViewCellEventHandler cellClickHandler;

        private void employeeManagmentSection(object? sender, EventArgs? e)
        {
            isEmployeeSectionActive = true;
            // Limpiar el panel derecho
            rightPanel.Controls.Clear();
            personalNames.Clear();
            personalList.Items.Clear();

            GetEmplooyesNamesOnSelector();
            GetAllWeeklyReportsPerEmployee();

            // Titulo de la sección 
            Label title = new Label();
            title.Text = "Reportes semanales";
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            title.Location = new Point(300, 50);
            title.Size = new Size(300, 50);
            rightPanel.Controls.Add(title);

            // Label de crear empleado
            Label description = new Label();
            description.Text = "Crear empleado";
            description.Font = new Font("Arial", 15, FontStyle.Regular);
            description.ForeColor = Color.Gray;
            description.Location = new Point(300, 100);
            description.Size = new Size(300, 50);
            rightPanel.Controls.Add(description);

            // Botón para crear un nuevo empleado
            Button newUserButton = new Button();
            newUserButton.Text = "Nuevo empleado";
            newUserButton.Font = new Font("Arial", 12, FontStyle.Regular);
            newUserButton.Location = new Point(350, 150);
            newUserButton.Size = new Size(200, 40);
            newUserButton.BackColor = Color.FromArgb(255, 204, 204);
            newUserButton.ForeColor = Color.FromArgb(31, 30, 68);
            newUserButton.FlatStyle = FlatStyle.Flat;
            newUserButton.FlatAppearance.BorderSize = 0;
            newUserButton.Click += new EventHandler(OpenCreateANewPerson);
            rightPanel.Controls.Add(newUserButton);

            // Icono de usuario
            IconPictureBox userIcon = new IconPictureBox();
            userIcon.IconChar = IconChar.UserAlt;
            userIcon.IconColor = Color.FromArgb(31, 30, 68);
            userIcon.Location = new Point(300, 150);
            userIcon.Size = new Size(32, 32);
            userIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(userIcon);

            // Icono de lista de empleados
            IconPictureBox iconDescription2 = new IconPictureBox();
            iconDescription2.IconChar = IconChar.Person;
            iconDescription2.IconColor = Color.FromArgb(31, 30, 68);
            iconDescription2.Location = new Point(300, 220);
            iconDescription2.Size = new Size(32, 32);
            iconDescription2.BackColor = Color.Transparent;
            rightPanel.Controls.Add(iconDescription2);

            Label description2 = new Label();
            description2.Text = "Reportes de empleado: ";
            description2.Font = new Font("Arial", 15, FontStyle.Regular);
            description2.ForeColor = Color.Gray;
            description2.Location = new Point(340, 220);
            description2.Size = new Size(250, 50); // Ajustar el tamaño del label
            rightPanel.Controls.Add(description2);

            // ComboBox para seleccionar empleados
            employeeSelector.Items.Clear(); // Limpiar los elementos del ComboBox
            employeeSelector.Location = new Point(description2.Right + 10, description2.Top);
            employeeSelector.Size = new Size(200, 50);
            employeeSelector.Font = new Font("Arial", 12, FontStyle.Regular);
            employeeSelector.ForeColor = Color.White;
            employeeSelector.BackColor = Color.FromArgb(31, 30, 68);
            employeeSelector.FlatStyle = FlatStyle.Flat;
            employeeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            employeeSelector.Items.Add("Selecciona un empleado"); // Opción inicial
            employeeSelector.SelectedIndex = 0; // Establecer como opción seleccionada por defecto
            employeeSelector.SelectedIndexChanged += new EventHandler(EmployeeSelector_SelectedIndexChanged);
            foreach (string name in personalNames)
            {
                employeeSelector.Items.Add(name);
            }
            rightPanel.Controls.Add(employeeSelector);

            // Icono de reportes semanales
            IconPictureBox iconDescription3 = new IconPictureBox();
            iconDescription3.IconChar = IconChar.CalendarAlt;
            iconDescription3.IconColor = Color.FromArgb(31, 30, 68);
            iconDescription3.Location = new Point(300, 270);
            iconDescription3.Size = new Size(32, 32);
            iconDescription3.BackColor = Color.Transparent;
            rightPanel.Controls.Add(iconDescription3);

            Label description3 = new Label();
            description3.Text = "Reportes semanales disponibles";
            description3.Font = new Font("Arial", 15, FontStyle.Regular);
            description3.ForeColor = Color.Gray;
            description3.Location = new Point(340, 270);
            description3.Size = new Size(250, 50);
            rightPanel.Controls.Add(description3);

            // ComboBox para seleccionar reportes semanales
            reportPerWeekSelector.Items.Clear(); // Limpiar los elementos del ComboBox
            reportPerWeekSelector.Location = new Point(description3.Right + 10, description3.Top);
            reportPerWeekSelector.Size = new Size(400, 50);
            reportPerWeekSelector.Font = new Font("Arial", 12, FontStyle.Regular);
            reportPerWeekSelector.ForeColor = Color.White;
            reportPerWeekSelector.BackColor = Color.FromArgb(31, 30, 68);
            reportPerWeekSelector.FlatStyle = FlatStyle.Flat;
            reportPerWeekSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            reportPerWeekSelector.Items.Add("Seleciona un reporte");
            reportPerWeekSelector.SelectedIndex = 0;
            reportPerWeekSelector.SelectedIndexChanged -= ReportPerWeekSelector_SelectedIndexChanged; // Desuscribirse del evento anterior
            reportPerWeekSelector.SelectedIndexChanged += new EventHandler(ReportPerWeekSelector_SelectedIndexChanged);
            foreach (string name in WeeklyReportsPerEmployee)
            {
                reportPerWeekSelector.Items.Add(name);
            }
            rightPanel.Controls.Add(reportPerWeekSelector);

            // Crear título para la tabla de reportes diarios
            Label dailyReportTitle = new Label();
            dailyReportTitle.Text = "Reportes diarios realizados";
            dailyReportTitle.Font = new Font("Arial", 20, FontStyle.Bold);
            dailyReportTitle.ForeColor = Color.Black;
            dailyReportTitle.Location = new Point(300, 340); // Posición del título
            dailyReportTitle.Size = new Size(300, 30);
            rightPanel.Controls.Add(dailyReportTitle);

            // Crear tabla para mostrar los reportes diarios
            dailyReportTable.Location = new Point(300, 390); // Bajar la tabla 10 unidades
            dailyReportTable.Size = new Size(rightPanel.Width - 350, rightPanel.Height - 400); // Ajustar el tamaño de la tabla con un margen de 10 unidades a la derecha y en la parte inferior
            dailyReportTable.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom; // Anclar la tabla a los lados izquierdo, derecho y parte inferior
            dailyReportTable.BackgroundColor = Color.FromArgb(31, 30, 68);
            dailyReportTable.ForeColor = Color.Black;
            dailyReportTable.BorderStyle = BorderStyle.FixedSingle;
            dailyReportTable.ReadOnly = true;
            dailyReportTable.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dailyReportTable.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            dailyReportTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 30, 68);
            dailyReportTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dailyReportTable.EnableHeadersVisualStyles = false;
            dailyReportTable.AllowUserToAddRows = false; // Evitar mostrar una fila vacía por defecto
            dailyReportTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (dailyReportTable.Columns.Count == 0)
            {
                dailyReportTable.Columns.Add("ID", "ID");
                dailyReportTable.Columns.Add("Day", "Día");
                dailyReportTable.Columns.Add("IncomeGenerated", "Ingreso Generado");
                dailyReportTable.Columns.Add("RefactionCost", "Costo de Refacción");
                dailyReportTable.Columns.Add("LaborCost", "Mano de Obra");
                dailyReportTable.Columns.Add("EmployeeCommission", "Comisión del Empleado");
                dailyReportTable.Columns.Add("CapturedDate", "Fecha Capturada");
            }

            // Desuscribirse de los eventos anteriores
            if (cellClickHandler != null)
            {
                dailyReportTable.CellClick -= cellClickHandler;
            }

            // Suscribirse al evento
            cellClickHandler = new DataGridViewCellEventHandler((sender, e) =>
            {
                // Manejar el evento CellClick aquí
            });
            dailyReportTable.CellClick += cellClickHandler;

            // Limpiar las filas de la tabla
            dailyReportTable.Rows.Clear();

            // Agregar la tabla al panel (asegurarse de no agregarla repetidamente)
            if (!rightPanel.Controls.Contains(dailyReportTable))
            {
                rightPanel.Controls.Add(dailyReportTable);
            }

        }

        private void DeleteEmployee(int id)
        {
            try
            {
                // Preguntar si está seguro de eliminar el empleado
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este empleado?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    DbConnect dbConnect = new DbConnect();
                    string query = "DELETE FROM person WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        dbConnect.OpenConnection();
                        cmd.ExecuteNonQuery();
                        dbConnect.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static class Prompt
        {
            public static string ShowDialog(string title, string promptText, string defaultValue = "")
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = title,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = promptText };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Text = defaultValue };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        //Modales para crear y editar un empleado
        public void OpenEditPersonModal(int id, string name)
        {
            using (EditPerson editPersonModal = new EditPerson(this, id, name))
            {
                editPersonModal.ShowDialog(Form1.ActiveForm);
                // FillEmployeeTable(employeeTable);
            }



        }
        private void OpenCreateANewPerson(object sender, EventArgs e)
        {
            using (CreateANewPersonModal createANewPerson = new CreateANewPersonModal(this))
            {
                createANewPerson.ShowDialog(Form1.ActiveForm);

                // Actualizar la tabla de empleados después de cerrar el modal
                employeeManagmentSection(null, null);
            }
        }
        //---------------------------------------------------------------
        private void ConfigurationSection(object sende, EventArgs e)
        {
            // Limpiar el panel derecho
            rightPanel.Controls.Clear();

            // Titulo del negocio
            Label title = new Label();
            title.Text = "Configuración";
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            title.Location = new Point(300, 50);
            title.Size = new Size(300, 50);
            rightPanel.Controls.Add(title);

            // Descripcion del negocio
            Label description = new Label();
            description.Text = "Configuraciones adicionales";
            description.Font = new Font("Arial", 15, FontStyle.Regular);
            description.ForeColor = Color.Gray;
            description.Location = new Point(300, 100);
            description.Size = new Size(300, 50);
            rightPanel.Controls.Add(description);

            // Icono de advertencia
            IconPictureBox warningIcon = new IconPictureBox();
            warningIcon.IconChar = IconChar.ExclamationTriangle;
            warningIcon.IconColor = Color.FromArgb(31, 30, 68);
            warningIcon.Location = new Point(300, 150);
            warningIcon.Size = new Size(32, 32);
            warningIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(warningIcon);

            // Boton para limpiar la base de datos
            Button cleanButton = new Button();
            cleanButton.Text = "Limpiar";
            cleanButton.Font = new Font("Arial", 12, FontStyle.Regular);
            cleanButton.Location = new Point(350, 150);
            cleanButton.Size = new Size(200, 40);
            cleanButton.BackColor = Color.FromArgb(255, 204, 204);
            cleanButton.ForeColor = Color.FromArgb(31, 30, 68);
            cleanButton.FlatStyle = FlatStyle.Flat;
            cleanButton.FlatAppearance.BorderSize = 0;
            cleanButton.Click += new EventHandler(CleanDataBase);
            rightPanel.Controls.Add(cleanButton);

            // Icono de respaldo de base de datos
            IconPictureBox backupIcon = new IconPictureBox();
            backupIcon.IconChar = IconChar.Database;
            backupIcon.IconColor = Color.FromArgb(31, 30, 68);
            backupIcon.Location = new Point(300, 200);
            backupIcon.Size = new Size(32, 32);
            backupIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(backupIcon);

            // Boton para respaldar la base de datos
            Button backupButton = new Button();
            backupButton.Text = "Respaldar base de datos";
            backupButton.Font = new Font("Arial", 12, FontStyle.Regular);
            backupButton.Location = new Point(350, 200);
            backupButton.Size = new Size(200, 40);
            backupButton.BackColor = Color.FromArgb(255, 204, 204);
            backupButton.ForeColor = Color.FromArgb(31, 30, 68);
            backupButton.FlatStyle = FlatStyle.Flat;
            backupButton.FlatAppearance.BorderSize = 0;
            backupButton.Click += new EventHandler(BackupDataBase);
            rightPanel.Controls.Add(backupButton);

            // Icono de off
            IconPictureBox offIcon = new IconPictureBox();
            offIcon.IconChar = IconChar.PowerOff;
            offIcon.IconColor = Color.FromArgb(31, 30, 68);
            offIcon.Location = new Point(300, 250);
            offIcon.Size = new Size(32, 32);
            offIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(offIcon);

            // Boton para cerrar la aplicación
            Button closeButton = new Button();
            closeButton.Text = "Salir del programa";
            closeButton.Font = new Font("Arial", 12, FontStyle.Regular);
            closeButton.Location = new Point(350, 250);
            closeButton.Size = new Size(200, 40);
            closeButton.BackColor = Color.FromArgb(255, 204, 204);
            closeButton.ForeColor = Color.FromArgb(31, 30, 68);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += new EventHandler(CloseApplication);
            rightPanel.Controls.Add(closeButton);



        }
        private void CloseApplication(object sender, EventArgs e)
        {
            //Preguntar al usuario si está seguro de cerrar la aplicación
            DialogResult result = MessageBox.Show("¿Está seguro de que desea cerrar la aplicación?", "Cerrar Aplicación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void AtentionSection(object sende, EventArgs e)
        {
            // Limpiar el panel derecho
            rightPanel.Controls.Clear();

            // Inicializar el DataGridView


            rightPanel.Controls.Add(dataGridView);

            //Titulo del negocio

            rightPanel.Controls.Add(title);

            //Titulo de filtrar por estado
            rightPanel.Controls.Add(descriptionFilter);

            //Descripcion del negocio

            rightPanel.Controls.Add(description);

            //Icono de busqueda
            IconPictureBox searchIcon = new IconPictureBox();
            searchIcon.IconChar = IconChar.Search;
            searchIcon.IconColor = Color.FromArgb(31, 30, 68);
            searchIcon.Location = new Point(300, 150);
            searchIcon.Size = new Size(32, 32);
            searchIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(searchIcon);

            //Campo de busqueda

            rightPanel.Controls.Add(search);

            //Selector para elegir si el buscador buscara por empleado o cliente
            rightPanel.Controls.Add(filterForEmployeeOrClient);

            //Selector de filtrado

            rightPanel.Controls.Add(filter);

            //Indidores de registros
            indicator.IconChar = IconChar.ChainBroken;
            indicator.IconColor = Color.Gray;
            indicator.Size = new Size(32, 32);
            float indicatorX = dataGridView.Location.X + (dataGridView.Width - indicator.Width) / 2.0f;
            float indicatorY = dataGridView.Location.Y + (dataGridView.Height - indicator.Height) / 2.0f;
            indicator.Location = new Point((int)indicatorX, (int)indicatorY);
            rightPanel.Controls.Add(indicator);
            indicator.BringToFront();

            // Configurar el Label
            indicatorLabel.Text = "Aún no hay registros.";
            indicatorLabel.TextAlign = ContentAlignment.MiddleCenter;
            indicatorLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            indicatorLabel.ForeColor = Color.Gray;
            indicatorLabel.AutoSize = true;
            float labelX = dataGridView.Location.X + (dataGridView.Width - indicatorLabel.Width) / 2.0f;
            float labelY = indicator.Bottom + 10;
            indicatorLabel.Location = new Point((int)labelX, (int)labelY);
            rightPanel.Controls.Add(indicatorLabel);
            indicatorLabel.BringToFront();

            // Crear el ComboBox para seleccionar la opción de fecha
            dateOptionSelector.Items.Clear();
            dateOptionSelector.Location = new Point(450, 100);
            dateOptionSelector.ForeColor = Color.White;
            dateOptionSelector.BackColor = Color.FromArgb(31, 30, 68);
            dateOptionSelector.Size = new Size(200, 30);
            dateOptionSelector.FlatStyle = FlatStyle.Flat;
            dateOptionSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            dateOptionSelector.Items.Add("Ninguna fecha");
            dateOptionSelector.Items.Add("Filtrar por fecha de reparación");
            dateOptionSelector.Items.Add("Filtrar por fecha recibido");
            dateOptionSelector.Items.Add("Filtrar por fecha de entrega");
            dateOptionSelector.SelectedIndex = 0; // Establecer "Ninguna fecha" como opción predeterminada
            dateOptionSelector.SelectedIndexChanged += new EventHandler(FilterButton_Click!);
            rightPanel.Controls.Add(dateOptionSelector);

            // Manejar el evento SelectedIndexChanged para mostrar u ocultar el DateTimePicker
            dateOptionSelector.SelectedIndexChanged += (sender, e) =>
            {
                if (dateOptionSelector.SelectedItem.ToString() == "Filtrar por fecha de reparación" || dateOptionSelector.SelectedItem.ToString() == "Filtrar por fecha recibido" || dateOptionSelector.SelectedItem.ToString() == "Filtrar por fecha de entrega")
                {
                    dateStartOfRange.Visible = true;
                }
                else
                {
                    dateStartOfRange.Visible = false;
                }
            };

            // Configurar el DateTimePicker para el filtrado de las 3 tipos de fechas
            dateStartOfRange.Location = new Point(450, 120); // Ajustar la posición del DateTimePicker
            dateStartOfRange.ForeColor = Color.White;
            dateStartOfRange.CalendarForeColor = Color.FromArgb(31, 30, 68);
            dateStartOfRange.Visible = false; // Ocultar el DateTimePicker por defecto
            dateStartOfRange.ValueChanged += new EventHandler(FilterButton_Click!);
            rightPanel.Controls.Add(dateStartOfRange);

            // Boton para filtrar los datos
            rightPanel.Controls.Add(slopeButton);
            rightPanel.Controls.Add(inLaboratoryButton);
            rightPanel.Controls.Add(repairedButton);
            rightPanel.Controls.Add(AllButton);

        }
        private void PerfilSection(object sende, EventArgs e)
        {
            // Limpiar el panel derecho
            rightPanel.Controls.Clear();

            // Titulo del negocio
            Label title = new Label();
            title.Text = "Perfil";
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            title.Location = new Point(300, 50);
            title.Size = new Size(300, 50);
            rightPanel.Controls.Add(title);

            // Descripcion del negocio
            Label description = new Label();
            description.Text = "Información del perfil";
            description.Font = new Font("Arial", 15, FontStyle.Regular);
            description.ForeColor = Color.Gray;
            description.Location = new Point(300, 100);
            description.Size = new Size(300, 50);
            rightPanel.Controls.Add(description);

            //Icono de usuario
            IconPictureBox userIconUser = new IconPictureBox();
            userIconUser.IconChar = IconChar.UserAlt;
            userIconUser.IconColor = Color.FromArgb(31, 30, 68);
            userIconUser.Location = new Point(300, 150);
            userIconUser.Size = new Size(32, 32);
            userIconUser.BackColor = Color.Transparent;
            rightPanel.Controls.Add(userIconUser);

            //Nombre de usuario
            Label userName = new Label();
            userName.Text = "Usuario: " + User;
            userName.Font = new Font("Arial", 12, FontStyle.Regular);
            userName.Location = new Point(350, 150);
            userName.Size = new Size(200, 50);
            rightPanel.Controls.Add(userName);

            //Icono de tipo de usuario
            IconPictureBox userIconUserType = new IconPictureBox();
            userIconUserType.IconChar = User == "Admin" ? IconChar.UserShield : IconChar.UserFriends;
            userIconUserType.IconColor = Color.FromArgb(31, 30, 68);
            userIconUserType.Location = new Point(300, 200);
            userIconUserType.Size = new Size(32, 32);
            userIconUserType.BackColor = Color.Transparent;
            rightPanel.Controls.Add(userIconUserType);

            //Tipo de usuario
            Label userType = new Label();
            userType.Text = "Tipo de usuario: " + (User == "Admin" ? "Administrador" : "Empleado");
            userType.Font = new Font("Arial", 12, FontStyle.Regular);
            userType.Location = new Point(350, 200);
            userType.Size = new Size(200, 50);
            rightPanel.Controls.Add(userType);


            //Icono de usuario
            IconPictureBox userIcon = new IconPictureBox();
            userIcon.IconChar = IconChar.User;
            userIcon.IconColor = Color.FromArgb(31, 30, 68);
            userIcon.Location = new Point(300, 350);
            userIcon.Size = new Size(32, 32);
            userIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(userIcon);

            Button cleanButton = new Button();
            cleanButton.Text = "Cerrar Sesión";
            cleanButton.Font = new Font("Arial", 12, FontStyle.Regular);
            cleanButton.Location = new Point(350, 350);
            cleanButton.Size = new Size(200, 40);
            cleanButton.BackColor = Color.FromArgb(255, 204, 204);
            cleanButton.ForeColor = Color.FromArgb(31, 30, 68);
            cleanButton.FlatStyle = FlatStyle.Flat;
            cleanButton.FlatAppearance.BorderSize = 0;
            cleanButton.Click += new EventHandler(CloseSession!);
            rightPanel.Controls.Add(cleanButton);
        }
        private void CloseSession(object sender, EventArgs e)
        {
            //Preguntar al usuario si está seguro de cerrar sesión
            DialogResult result = MessageBox.Show("¿Está seguro de que desea cerrar sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                leftPanel.Controls.Clear();
                rightPanel.Controls.Clear();
                OpenLoginForm();
                PerfilSection(null!, null!);
                RebuildLeftPanel(null!, null!);

            }
        }
        private void RebuildLeftPanel(object sender, EventArgs e)
        {
            // Limpiar el panel izquierdo
            leftPanel.Controls.Clear();
            if (User == "Admin")
            {
                leftPanel.Controls.Add(icon0);
                leftPanel.Controls.Add(icon1);
            }

            leftPanel.Controls.Add(icon2);
            icon3.IconChar = User == "Admin" ? IconChar.UserShield : IconChar.UserFriends;
            icon3.IconColor = Color.White;
            icon3.Dock = DockStyle.Top;
            icon3.FlatStyle = FlatStyle.Flat;
            icon3.FlatAppearance.BorderSize = 0;
            icon3.Text = User == "Admin" ? "Administrador" : "Empleado";
            icon3.TextImageRelation = TextImageRelation.ImageBeforeText;
            icon3.Font = new Font("Arial", 12, FontStyle.Bold);
            icon3.ForeColor = Color.White;
            icon3.Height = 60;
            icon3.Click += new EventHandler(PerfilSection!);
            leftPanel.Controls.Add(icon3);

            Panel sizeBox = new Panel();
            sizeBox.Dock = DockStyle.Top;
            sizeBox.Height = 40;
            sizeBox.BackColor = Color.FromArgb(31, 30, 68);
            leftPanel.Controls.Add(sizeBox);

            leftPanel.Controls.Add(ButtonAddCustomer);
            Panel sizeBox2 = new Panel();
            sizeBox2.Dock = DockStyle.Bottom;
            sizeBox2.Height = 40;
            sizeBox2.BackColor = Color.FromArgb(31, 30, 68);
            leftPanel.Controls.Add(sizeBox2);


        }
        private void CleanDataBase(object sender, EventArgs e)
        {
            // Verificar si el usuario es administrador
            if (User == "Admin" && Password == "Admin@123")
            {
                // Preguntar al usuario si está seguro de limpiar la base de datos
                DialogResult result = MessageBox.Show("¿Está seguro de que desea limpiar la base de datos? Todos los registros se eliminarán y no será reversible", "Confirmar limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Instanciar la clase DbConnect y ejecutar las consultas de eliminación
                        DbConnect dbConnect = new DbConnect();
                        // Eliminar registros de la tabla daily_report
                        string queryReport = "DELETE FROM daily_report";
                        dbConnect.ExecuteQuery(queryReport);

                        // Eliminar registros de la tabla report_per_week
                        string queryReportPerWeek = "DELETE FROM report_per_week";
                        dbConnect.ExecuteQuery(queryReportPerWeek);

                        // Eliminar registros de la tabla customers
                        string queryCustomers = "DELETE FROM customers";
                        dbConnect.ExecuteQuery(queryCustomers);

                        // Eliminar registros de la tabla employees
                        string queryPerson = "DELETE FROM employees";
                        dbConnect.ExecuteQuery(queryPerson);



                        // Actualizar el DataGridView después de limpiar la base de datos
                        GetAllRegisters(null!, null!);

                        MessageBox.Show("Se limpió correctamente la base de datos", "Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al limpiar la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No tienes permisos para limpiar la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public void BackupDataBase(Object sender, EventArgs e)
        {
            if (User == "Admin" && Password == "Admin@123")
            {
                try
                {
                    // Crear un SaveFileDialog para que el usuario seleccione la ubicación y el nombre del archivo de respaldo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "SQL Files (*.sql)|*.sql";
                    saveFileDialog.Title = "Guardar respaldo de la base de datos";
                    saveFileDialog.FileName = "backup_tekno_" + DateTime.Now.ToString("dd_MMMM_yyyy", new System.Globalization.CultureInfo("es-ES")) + ".sql";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Obtener la ruta del archivo seleccionada por el usuario
                        string backupFilePath = saveFileDialog.FileName;

                        // Obtener la configuración de conexión desde DbConnect
                        DbConnect dbConnect = new DbConnect();
                        string server = dbConnect.server;
                        string database = dbConnect.database;
                        string user = dbConnect.user;
                        string password = dbConnect.password;

                        // Ruta completa al ejecutable mysqldump
                        // string mysqldumpPath = @"C:\Program Files\MariaDB 11.4\bin\mysqldump.exe"; // Ajusta esta ruta según tu instalación
                        string mysqldumpPath = @"C:\Program Files\MariaDB 10.5\bin\mysqldump.exe";
                        // Comando para respaldar la base de datos
                        string backupCommand = $"\"{mysqldumpPath}\" --user={user} --password={password} --host={server} {database} --result-file=\"{backupFilePath}\"";

                        // Ejecutar el comando de respaldo
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                        psi.FileName = "cmd.exe";
                        psi.RedirectStandardInput = true;
                        psi.RedirectStandardOutput = true;
                        psi.RedirectStandardError = true;
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;

                        using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(psi))
                        {
                            using (System.IO.StreamWriter sw = process.StandardInput)
                            {
                                if (sw.BaseStream.CanWrite)
                                {
                                    sw.WriteLine(backupCommand);
                                }
                            }
                            string output = process.StandardOutput.ReadToEnd();
                            string error = process.StandardError.ReadToEnd();
                            process.WaitForExit();

                            if (!string.IsNullOrEmpty(error))
                            {
                                MessageBox.Show("Error al realizar el respaldo de la base de datos: " + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        MessageBox.Show("Respaldo de la base de datos realizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar el respaldo de la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. No tiene permiso para realizar esta acción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //--------------------------------------------------------------------

        //Obtener los datos del empleado seleccionado
        private void EmployeeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllWeeklyReportsPerEmployee();
        }
        private void ReportPerWeekSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reportPerWeekSelector.SelectedIndex == 0)
            {
                // Limpiar la tabla si se selecciona la opción por defecto
                dailyReportTable.Rows.Clear();
            }
            else if (reportPerWeekSelector.SelectedIndex > 0)
            {
                string selectedReport = reportPerWeekSelector.SelectedItem.ToString();
                string[] fechas = selectedReport.Split(new string[] { " a " }, StringSplitOptions.None);
                DateTime startDate = DateTime.ParseExact(fechas[0], "dd 'de' MMMM 'del' yyyy", new System.Globalization.CultureInfo("es-ES"));
                DateTime endDate = DateTime.ParseExact(fechas[1], "dd 'de' MMMM 'del' yyyy", new System.Globalization.CultureInfo("es-ES"));

                int getIdEmployee = GetIdEmployeeByName(employeeSelector.SelectedItem.ToString());
                int idWeeklyReport = GetIdWeeklyReport(startDate, endDate, getIdEmployee);

                // Desuscribirse del evento CellClick antes de actualizar la tabla
                if (cellClickHandler != null)
                {
                    dailyReportTable.CellClick -= cellClickHandler;
                }
                GetAllDailyReportsByIdWeeklyReport(idWeeklyReport);
                cellClickHandler = new DataGridViewCellEventHandler((sender, e) =>
                {
                    // Manejar el evento CellClick aquí
                });
                dailyReportTable.CellClick += cellClickHandler;
            }
        }

        public void GetEmplooyesNamesOnSelector()
        {
            try
            {
                DbConnect dbConnect = new DbConnect();
                string query = "SELECT nombre FROM employees";
                DataTable dataTable = dbConnect.ExecuteQuery(query);

                foreach (DataRow row in dataTable.Rows)
                {
                    string name = row["nombre"].ToString();
                    personalNames.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener nombres de empleados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public int GetIdEmployeeByName(string name)
        {
            int idEmpleado = -1; // Valor por defecto en caso de error
            try
            {
                DbConnect dbConnect = new DbConnect();
                string queryGetIdEmployee = "SELECT id_empleado FROM employees WHERE nombre = @name";
                using (MySqlCommand cmd = new MySqlCommand(queryGetIdEmployee, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);

                    dbConnect.OpenConnection();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idEmpleado = reader.GetInt32("id_empleado");
                        }
                    }
                    dbConnect.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el ID del empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return idEmpleado;
        }
        public void GetAllWeeklyReportsPerEmployee()
        {
            try
            {
                if (employeeSelector.SelectedItem == null)
                {
                    // MessageBox.Show("Por favor, seleccione un empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string employeeName = employeeSelector.SelectedItem.ToString();
                int id_employee = GetIdEmployeeByName(employeeName);
                DbConnect dbConnect = new DbConnect();
                string queryGetWeeklyReport = "SELECT fecha_inicio, fecha_final FROM report_per_week WHERE id_empleado = @id_employee";
                using (MySqlCommand cmd = new MySqlCommand(queryGetWeeklyReport, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@id_employee", id_employee);
                    dbConnect.OpenConnection();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<string> reportes = new List<string>();
                        while (reader.Read())
                        {
                            DateTime fechaInicio = reader.GetDateTime("fecha_inicio");
                            DateTime fechaFinal = reader.GetDateTime("fecha_final");
                            string fechaFormateada = $"{fechaInicio.ToString("dd 'de' MMMM 'del' yyyy", new System.Globalization.CultureInfo("es-ES"))} a {fechaFinal.ToString("dd 'de' MMMM 'del' yyyy", new System.Globalization.CultureInfo("es-ES"))}";
                            reportes.Add(fechaFormateada);
                        }

                        // Invertir la lista de reportes
                        reportes.Reverse();

                        // Limpiar los elementos del ComboBox y agregar los reportes en orden inverso
                        reportPerWeekSelector.Items.Clear();
                        reportPerWeekSelector.Items.Add("Selecciona un reporte");
                        reportPerWeekSelector.SelectedIndex = 0;
                        foreach (string reporte in reportes)
                        {
                            reportPerWeekSelector.Items.Add(reporte);
                        }
                    }
                    dbConnect.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener todos los reportes semanales de un empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        public int GetIdWeeklyReport(DateTime startDate, DateTime endDate, int idEmployee)
        {
            int idWeeklyReport = -1; // Valor por defecto en caso de error
            try
            {
                DbConnect dbConnect = new DbConnect();
                string queryGetIdWeeklyReport = "SELECT id_reporte_semana FROM report_per_week WHERE id_empleado = @idEmployee AND fecha_inicio = @startDate AND fecha_final = @endDate";
                using (MySqlCommand cmd = new MySqlCommand(queryGetIdWeeklyReport, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@idEmployee", idEmployee);

                    dbConnect.OpenConnection();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idWeeklyReport = reader.GetInt32("id_reporte_semana");
                        }
                    }
                    dbConnect.CloseConnection();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el ID del reporte semanal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return idWeeklyReport;
        }
        public void GetAllDailyReportsByIdWeeklyReport(int idReport)
        {
            try
            {
                DbConnect dbConnect = new DbConnect();
                string queryGetDailyReports = "SELECT id_reporte_diario, dia, ingreso_generado, costo_refaccion, mano_obra, comision_empleado, fecha_capturada FROM daily_report WHERE id_reporte_semana = @idReport";
                using (MySqlCommand cmd = new MySqlCommand(queryGetDailyReports, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@idReport", idReport);
                    dbConnect.OpenConnection();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dailyReportTable.Rows.Clear(); // Limpiar los elementos de la tabla
                        decimal totalComision = 0; // Variable para almacenar el total de la comisión

                        int rowIndex = 0;
                        while (reader.Read())
                        {
                            int idReporteDiario = reader.GetInt32("id_reporte_diario");
                            string dia = reader.GetString("dia");
                            decimal ingresoGenerado = reader.GetDecimal("ingreso_generado");
                            decimal costoRefaccion = reader.GetDecimal("costo_refaccion");
                            decimal manoObra = reader.GetDecimal("mano_obra");
                            decimal comisionEmpleado = reader.GetDecimal("comision_empleado");
                            DateTime fechaCapturada = reader.GetDateTime("fecha_capturada");

                            // Formatear la fecha capturada
                            string amPm = fechaCapturada.Hour >= 12 ? "PM" : "AM";
                            string fechaCapturadaFormateada = fechaCapturada.ToString("dd 'de' MMMM 'del' yyyy 'a las' hh:mm", new System.Globalization.CultureInfo("es-ES")) + " " + amPm;

                            // Formatear los valores monetarios
                            string ingresoGeneradoFormateado = ingresoGenerado.ToString("C", new System.Globalization.CultureInfo("es-MX"));
                            string costoRefaccionFormateado = costoRefaccion.ToString("C", new System.Globalization.CultureInfo("es-MX"));
                            string manoObraFormateado = manoObra.ToString("C", new System.Globalization.CultureInfo("es-MX"));
                            string comisionEmpleadoFormateado = comisionEmpleado.ToString("C", new System.Globalization.CultureInfo("es-MX"));

                            dailyReportTable.Rows.Add(idReporteDiario, dia, ingresoGeneradoFormateado, costoRefaccionFormateado, manoObraFormateado, comisionEmpleadoFormateado, fechaCapturadaFormateada);

                            // Aplicar estilo a las filas pares
                            if (rowIndex % 2 == 0)
                            {
                                DataGridViewRow row = dailyReportTable.Rows[rowIndex];
                                row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                                row.DefaultCellStyle.ForeColor = Color.Black;
                            }

                            // Sumar la comisión del empleado al total
                            totalComision += comisionEmpleado;
                            rowIndex++;
                        }

                        // Agregar una fila al final con el total de la comisión
                        string totalComisionFormateado = totalComision.ToString("C", new System.Globalization.CultureInfo("es-MX"));
                        int totalRowIndex = dailyReportTable.Rows.Add(null, null, null, null, "Total Comisión", totalComisionFormateado);

                        // Aplicar estilo a la fila de total
                        DataGridViewRow totalRow = dailyReportTable.Rows[totalRowIndex];
                        totalRow.Cells["LaborCost"].Style.ForeColor = Color.Green;
                        totalRow.Cells["EmployeeCommission"].Style.ForeColor = Color.Green;
                        totalRow.Cells["LaborCost"].Style.Font = new Font(dailyReportTable.Font, FontStyle.Bold);
                        totalRow.Cells["EmployeeCommission"].Style.Font = new Font(dailyReportTable.Font, FontStyle.Bold);
                    }
                    dbConnect.CloseConnection();
                }

                // Manejar los eventos
                dailyReportTable.CellClick -= cellClickHandler!;
                dailyReportTable.CellClick += new DataGridViewCellEventHandler(cellClickHandler!);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los reportes diarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //-------------------------------------------------------------------

        private void FilterButton_Click(object sender, EventArgs e)
        {
            // Validar que la fecha inicial no sea mayor que la fecha final si los DateTimePicker están visibles
            if (dateStartOfRange.Visible && dateFinalOfRange.Visible)
            {
                if (dateStartOfRange.Value.Date > dateFinalOfRange.Value.Date)
                {
                    MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string selectedFilter = filter.SelectedItem?.ToString()!;

            if (string.IsNullOrEmpty(selectedFilter))
            {
                MessageBox.Show("Por favor, selecciona un filtro.");
                return;
            }

            // Ejecutar el filtrado con los valores válidos
            GetFilterRegisters(selectedFilter, search.Text);
        }
        private void OpenUpdateDate_Click(DateTime startDate, DateTime endDate, int id)
        {
            using (ModifyReportDate customerForm = new ModifyReportDate(this, startDate, endDate, id))
            {
                customerForm.ShowDialog(Form1.ActiveForm);
            }
        }
        private void OpenLoginForm()
        {

            // Abrir el formulario de inicio de sesión
            using (AccountForm loginForm = new AccountForm(this))
            {
                loginForm.ShowDialog();
            }
        }
        private void OpenCustomerFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (CustomerForm customerForm = new CustomerForm(this))
                {
                    if (customerForm.ShowDialog(this) == DialogResult.OK)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OpenRepairFormButton_Click(object sender, EventArgs e, int id, string name, string tipoDispositivo, string brand, string model, string problem, string statusNow, string fechaEntregar, string comentarios)
        {
            using (RepairDeviceModal customerForm = new RepairDeviceModal(
                this,
                id: id,
                name: name,
                tipoDispositivo: tipoDispositivo,
                brand: brand,
                model: model,
                problem: problem,
                statusNow: statusNow,
                fechaEntregar: fechaEntregar,
                comment: comentarios))
            {
                customerForm.ShowDialog(Form1.ActiveForm);
            }
        }
        private void OpenConfirmDeliverdFinished_Click(object sender, EventArgs e, int id, string name, string tipoDispositivo, string brand, string model, string problem, string statusNow, string fechaReparado, string costo, string diagnostico, string personaReparo, string personaRecibio, string fechaRecibido, string comentarios, string refaccion)
        {
            using (ConfirmFinishDelivered customerForm = new ConfirmFinishDelivered(
                this,
                id: id,
                name: name,
                tipo_dispositivo: tipoDispositivo,
                brand: brand,
                model: model,
                problem: problem,
                status: statusNow,
                fechaReparado: fechaReparado,
                costo: costo,
                diagnostico: diagnostico,
                personaReparo: personaReparo,
                personaRecibio: personaRecibio,
                fechaRecibido: fechaRecibido,
                comment: comentarios, // Nuevo campo
                refaccion: refaccion))   // Nuevo campo
            {
                customerForm.ShowDialog(Form1.ActiveForm);
            }
        }
        private void OpenDeliveredToLaboratoryModalButton_Click(object sender, EventArgs e, int id, string name, string brand, string model, string status, string problem, string fechaRecibido, string fechaEntregar, string comentarios)
        {
            using (DeliveredToLaboratoryModal deliveredModal = new DeliveredToLaboratoryModal(
                this,
                id,
                name,
                brand,
                model,
                status,
                problem,
                fechaRecibido,
                fechaEntregar,
                comentarios
                ))
            {
                deliveredModal.ShowDialog(Form1.ActiveForm);
            }
        }
        private void ConfirmDelivered(int id)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de que desea marcar este dispositivo como entregado?", "Confirmar entrega", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DbConnect dbConnect = new DbConnect();

                string query = $"UPDATE customers SET estatus = 'ENTREGADO' WHERE id = {id}";
                dbConnect.ExecuteQuery(query);

                GetFilterRegisters(filter.Text, search.Text);

                MessageBox.Show("El dispositivo se entregó", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La entrega ha sido cancelada.", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ConfirmFinished(int id)
        {
            DialogResult result = MessageBox.Show("Este dispositivo ya a concluido", "Confirmar finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                // MessageBox.Show("Este dispositivo ya ha sido entregado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpenEditCustomerModal(string name, string phone, string brand, string model, string reason, string date, string hour, string status, string comment)
        {
            using (EditCustomer editCustomerModal = new EditCustomer(this, name, phone, brand, model, reason, date, hour, status, comment))
            {
                editCustomerModal.ShowDialog(Form1.ActiveForm);
            }

        }


        private void DeleteRecordById(int id)
        {

            // Preguntar al usuario si está seguro de eliminar el registro
            DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes && User == "Admin" && Password == "Admin@123")
            {
                // Instanciar la clase DbConnect y ejecutar la consulta de eliminación
                DbConnect dbConnect = new DbConnect();
                string query = $"DELETE FROM customers WHERE id = {id}";
                dbConnect.ExecuteQuery(query);

                // Actualizar el DataGridView después de eliminar el registro
                GetFilterRegisters(filter.Text, search.Text);

                MessageBox.Show("Se eliminó correctamente el cliente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No tienes permisos para eliminar registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void UpdateStatusRecordById(int id)
        {
            // Preguntar al usuario si está seguro de realizar la entrega
            DialogResult result = MessageBox.Show("¿Está seguro de que desea realizar la entrega?", "Confirmar entrega", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Instanciar la clase DbConnect y ejecutar la consulta de actualización
                DbConnect dbConnect = new DbConnect();

                // Cambiar 'ENTREGADO' por el estatus que deseas asignar
                string query = $"UPDATE customers SET estatus = 'ENTREGADO' WHERE id = {id}";
                dbConnect.ExecuteQuery(query);

                // Actualizar el DataGridView después de la actualización del registro
                GetAllRegisters(null!, null!);

                MessageBox.Show("Entrega realizada con éxito", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        // Si el usuario presiona la tecla ENTER en el campo de busqueda se ejecutara el evento FilterButton_Click
        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FilterButton_Click(sender, e);
            }
        }

        public void ExecutePeriodicTask()
        {
            // Aquí va el código que deseas ejecutar cada minuto
            // Por ejemplo, habilitar el programador de eventos
            DbConnect dbConnect = new DbConnect();
            string query = "SET GLOBAL event_scheduler = ON";

            try
            {
                dbConnect.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error enabling event scheduler: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}