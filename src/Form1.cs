using FontAwesome.Sharp;
using System.Drawing;
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;


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
        DataGridView employeeTable = new DataGridView();
        DataGridView reportTable = new DataGridView();

        public ComboBox filterForEmployeeOrClient = new ComboBox();

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
            dataGridView.Size = new Size((int)(Width * 1.1), (int)(Height * 1));
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
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
            description.Text = "Filtra y atiende";
            description.Font = new Font("Arial", 15, FontStyle.Regular);
            description.ForeColor = Color.Gray;
            description.Location = new Point(300, 100);
            description.Size = new Size(300, 50);
            rightPanel.Controls.Add(description);

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
            inLaboratoryButton.Text = "En el labroatorio";
            inLaboratoryButton.Font = new Font("Arial", 12, FontStyle.Regular);
            inLaboratoryButton.Location = new Point(910, 150);
            inLaboratoryButton.Size = new Size(200, 40);
            inLaboratoryButton.BackColor = Color.FromArgb(31, 30, 68);
            inLaboratoryButton.ForeColor = Color.White;
            inLaboratoryButton.FlatStyle = FlatStyle.Flat;
            inLaboratoryButton.FlatAppearance.BorderSize = 0;
            inLaboratoryButton.Click += new EventHandler(filterButtons.LaboratoryButton_Click);
            rightPanel.Controls.Add(inLaboratoryButton);

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

            // Inicializar el Timer
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 60000; // 1 minuto en milisegundos
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }



        // Evento para manejar el cambio de selección en el ComboBox
        private void FilterForEmployeeOrClient(object sender, EventArgs e)
        {
            filterType = filterForEmployeeOrClient.SelectedItem.ToString();
        }
        public void GetFilterRegisters(string filterSelect, string search)
        {
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
                    // Verificar si la consulta ya tiene una cláusula WHERE
                    if (query.Contains("WHERE"))
                    {
                        if (filterType == "Cliente")
                        {
                            query += " AND nombre LIKE @search";
                        }
                        else if (filterType == "Empleado")
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
                    { "@search", "%" + search + "%" }
                };

                DataTable dataTable = dbConnect.ExecuteQuery(query, parameters);

                dataTable.Columns["id"]!.ColumnName = "ID";
                dataTable.Columns["nombre"]!.ColumnName = "Nombre";
                dataTable.Columns["telefono"]!.ColumnName = "Teléfono";
                dataTable.Columns["tipo_dispositivo"]!.ColumnName = "Tipo de Dispositivo";
                dataTable.Columns["marca"]!.ColumnName = "Marca";
                dataTable.Columns["modelo"]!.ColumnName = "Modelo";
                dataTable.Columns["estatus"]!.ColumnName = "Estatus";
                dataTable.Columns["fecha_entregar"]!.ColumnName = "Fecha de Entrega";
                dataTable.Columns["motivo"]!.ColumnName = "Motivo";
                dataTable.Columns["persona_recibio"]!.ColumnName = "Persona que Recibió";
                dataTable.Columns["fecha_recibido"]!.ColumnName = "Fecha de Recepción";
                dataTable.Columns["persona_reparo"]!.ColumnName = "Persona que Reparó";
                dataTable.Columns["diagnostico"]!.ColumnName = "Diagnóstico";
                dataTable.Columns["fecha_reparado"]!.ColumnName = "Fecha de Reparación";
                dataTable.Columns["costo"]!.ColumnName = "Costo";

                // Eliminar la columna 'tiempo_restante' del DataTable
                if (dataTable.Columns.Contains("tiempo_restante"))
                {
                    dataTable.Columns.Remove("tiempo_restante");
                }

                // Asignar los datos al DataGridView
                dataGridView.DataSource = dataTable;
                dataGridView.AllowUserToAddRows = false;

                // Manejar los eventos
                dataGridView.CellFormatting -= dataGridView_CellFormatting!;
                dataGridView.CellClick -= dataGridView_CellClick!;

                dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting!);
                dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick!);
            }
        }
        public void GetAllRegisters(object sender, EventArgs e)
        {

            // Instanciar la clase DbConnect y ejecutar la consulta
            DbConnect dbConnect = new DbConnect();
            string query = "SELECT * FROM customers WHERE estatus IN ('PENDIENTE', 'ATRASADO') ORDER BY fecha_recibido DESC";

            // Crear las columnas de botones "Acciones" , "Eliminar" y "Editar" antes de asignar los datos
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
                eliminarColumn.Text = "Borrar";
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
            dataTable.Columns["nombre"]!.ColumnName = "Nombre";
            dataTable.Columns["telefono"]!.ColumnName = "Teléfono";
            dataTable.Columns["tipo_dispositivo"]!.ColumnName = "Tipo de Dispositivo";
            dataTable.Columns["marca"]!.ColumnName = "Marca";
            dataTable.Columns["modelo"]!.ColumnName = "Modelo";
            dataTable.Columns["estatus"]!.ColumnName = "Estatus";
            dataTable.Columns["fecha_entregar"]!.ColumnName = "Fecha de Entrega";
            dataTable.Columns["motivo"]!.ColumnName = "Motivo";
            dataTable.Columns["persona_recibio"]!.ColumnName = "Persona que Recibió";
            dataTable.Columns["fecha_recibido"]!.ColumnName = "Fecha de Recepción";
            dataTable.Columns["persona_reparo"]!.ColumnName = "Persona que Reparó";
            dataTable.Columns["diagnostico"]!.ColumnName = "Diagnóstico";
            dataTable.Columns["fecha_reparado"]!.ColumnName = "Fecha de Reparación";
            dataTable.Columns["costo"]!.ColumnName = "Costo";

            // Asignar los datos al DataGridView
            dataGridView.DataSource = dataTable;
            // Deshabilitar la opción de agregar nuevas filas
            dataGridView.AllowUserToAddRows = false;

            // Manejar los eventos
            dataGridView.CellFormatting -= dataGridView_CellFormatting!;
            dataGridView.CellClick -= dataGridView_CellClick!;

            // Manejar el evento CellFormatting para cambiar el color del texto de la columna "estatus"
            dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting!);

            // Manejar el evento CellClick para capturar los clics en los botones de la columna "Acciones"
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
                    string name = dataGridView.Rows[e.RowIndex].Cells["Nombre"].Value.ToString()!;
                    string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                    string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                    string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                    string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                    string fechaEntregar = dataGridView.Rows[e.RowIndex].Cells["Fecha de Entrega"].Value.ToString()!;

                    // Verificar si el modal ya está abierto
                    if (Application.OpenForms["DeliveredModal"] == null)
                    {
                        OpenDeliveredModalButton_Click(sender, e, id, name, brand, model, statusNow, motivo, tipoDispositivo, fechaEntregar);
                    }
                }
                else if (statusNow == "EN LABORATORIO")
                {
                    // Aquí puedes agregar la lógica que deseas ejecutar cuando se haga clic en el botón
                    // Pasar el datos de ese cliente al formulario de atención
                    int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                    string name = dataGridView.Rows[e.RowIndex].Cells["Nombre"].Value.ToString()!;
                    string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                    string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                    string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                    string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                    string fechaEntregar = dataGridView.Rows[e.RowIndex].Cells["Fecha de Entrega"].Value.ToString()!;

                    // string problem = dataGridView.Rows[e.RowIndex].Cells["problema"].Value.ToString();

                    OpenAtentionFormButton_Click(sender, e, id, name, tipoDispositivo, brand, model, motivo, statusNow, fechaEntregar);
                }
                else if (statusNow == "ENTREGADO")
                {
                    // Ejecutar la función X para otros estados
                    ConfirmFinished(Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["id"].Value));

                }
                else
                {


                    int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                    string name = dataGridView.Rows[e.RowIndex].Cells["Nombre"].Value.ToString()!;
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

                    OpenConfirmDeliverdFinished_Click(sender, e, id, name, tipoDispositivo, brand, model, motivo, statusNow, fechaReparado, costo, diagnostico, personaReparo, personaRecibio, fechaRecibido);

                }
            }

            // Verificar si el clic fue en la columna de eliminar
            if (e.ColumnIndex == dataGridView.Columns["Eliminar"].Index && e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                // Obtener el valor de la celda de la fila correspondiente
                var cellValue = dataGridView.Rows[e.RowIndex].Cells["Eliminar"].Value;
                // Aquí puedes agregar la lógica que deseas ejecutar cuando se haga clic en el botón
                // Pasar el datos de ese cliente al formulario de atención
                int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);

                DeleteRecordById(id);
            }
            // Verificar si el clic fue en la columna de editar
            if (e.ColumnIndex == dataGridView.Columns["Editar"].Index && e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
            {
                // Obtener el valor de la celda de la fila correspondiente
                var cellValue = dataGridView.Rows[e.RowIndex].Cells["Editar"].Value;
                // Aquí puedes agregar la lógica que deseas ejecutar cuando se haga clic en el botón
                // Pasar el datos de ese cliente al formulario de atención
                int id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                string phone = dataGridView.Rows[e.RowIndex].Cells["Teléfono"].Value.ToString()!;
                string name = dataGridView.Rows[e.RowIndex].Cells["Nombre"].Value.ToString()!;
                string tipoDispositivo = dataGridView.Rows[e.RowIndex].Cells["Tipo de Dispositivo"].Value.ToString()!;
                string brand = dataGridView.Rows[e.RowIndex].Cells["Marca"].Value.ToString()!;
                string model = dataGridView.Rows[e.RowIndex].Cells["Modelo"].Value.ToString()!;
                string motivo = dataGridView.Rows[e.RowIndex].Cells["Motivo"].Value.ToString()!;
                string fechaEntregar = dataGridView.Rows[e.RowIndex].Cells["Fecha de Entrega"].Value.ToString()!;
                string statusNow = dataGridView.Rows[e.RowIndex].Cells["Estatus"].Value.ToString()!;

                //Separar la fecha de entrega en fecha y hora
                string[] fechaEntregarArray = fechaEntregar.Split(' ');
                string fecha = fechaEntregarArray[0];
                string hora = fechaEntregarArray[1];


                OpenEditCustomerModal(name, phone, brand, model, motivo, fecha, hora, statusNow);
            }
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Costo" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal costo))
                {
                    e.Value = costo.ToString("N0"); // Formatear con comas para miles
                    e.FormattingApplied = true;
                }
            }
            //Pintar las celdas segun el estatus, pintar la columna fecha_entregar 

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
                            e.CellStyle!.ForeColor = Color.DarkRed;
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
            // Pintar el texto de la columna fecha_entregar según todos los estatus (PENDIENTE, ATRASADO, REPARADO, ENTREGADO, NO REPARADO, EN LABORATORIO)
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
                //Switch para cambiar el texto del boton segun el estatus
                switch (estatus)
                {
                    case "PENDIENTE":
                        e.Value = "Atender";
                        e.CellStyle!.BackColor = Color.Orange;
                        break;
                    case "ATRASADO":
                        e.Value = "Atender";
                        e.CellStyle!.BackColor = Color.Red;
                        break;
                    case "REPARADO":
                        e.Value = "Entregar";
                        e.CellStyle!.BackColor = Color.Green;
                        break;
                    case "ENTREGADO":
                        e.Value = "Finalizado";
                        e.CellStyle!.BackColor = Color.DarkCyan;
                        break;
                    case "NO REPARADO":
                        e.Value = "Entregar";
                        e.CellStyle!.BackColor = Color.DarkRed;
                        break;
                    case "EN LABORATORIO":
                        e.Value = "Reparar";
                        e.CellStyle!.BackColor = Color.DarkBlue;
                        break;

                }

            }
            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Entrega")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fechaEntrega))
                {
                    e.Value = fechaEntrega.ToString("MM/dd/yyyy   hh:mm tt"); // Formato de 12 horas con AM/PM
                    e.FormattingApplied = true;
                }
                e.CellStyle.BackColor = Color.CadetBlue;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Recepción" || dataGridView.Columns[e.ColumnIndex].Name == "Persona que Recibió")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fechaRecibido))
                {
                    e.Value = fechaRecibido.ToString("MM/dd/yyyy hh:mm tt"); // Formato de 12 horas con AM/PM
                    e.FormattingApplied = true;
                }
                e.CellStyle.BackColor = Color.CadetBlue;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Fecha de Reparación" || dataGridView.Columns[e.ColumnIndex].Name == "Persona que Reparó" || dataGridView.Columns[e.ColumnIndex].Name == "Diagnóstico" || dataGridView.Columns[e.ColumnIndex].Name == "Costo")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime fechaReparado))
                {
                    e.Value = fechaReparado.ToString("MM/dd/yyyy hh:mm tt"); // Formato de 12 horas con AM/PM
                    e.FormattingApplied = true;
                }
                e.CellStyle.BackColor = Color.RoyalBlue;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Nombre" || dataGridView.Columns[e.ColumnIndex].Name == "Teléfono")
            {
                e.CellStyle.BackColor = Color.DarkCyan;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Tipo de Dispositivo" || dataGridView.Columns[e.ColumnIndex].Name == "Marca" || dataGridView.Columns[e.ColumnIndex].Name == "Modelo")
            {
                e.CellStyle.BackColor = Color.RoyalBlue;
                e.CellStyle.ForeColor = Color.White;
            }

            if (dataGridView.Columns[e.ColumnIndex].Name == "Motivo")
            {
                e.CellStyle.BackColor = Color.DarkRed;
                e.CellStyle.ForeColor = Color.White;
            }
            if (dataGridView.Columns[e.ColumnIndex].Name == "Costo" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal costo))
                {
                    e.Value = $"${costo:N0}"; // Formatear con signo $ y comas para miles
                    e.FormattingApplied = true;
                }
            }





        }

        //Sección de reportes, contiene dos tablas, una para los empleados y otra para los reportes semanales
        private DataGridViewCellEventHandler cellClickHandler;

        private void employeeManagmentSection(object sender, EventArgs e)
        {
            isEmployeeSectionActive = true;
            // Limpiar los salarios si es lunes
            // ResetSalariesIfMonday();

            // Limpiar el panel derecho
            rightPanel.Controls.Clear();
            personalNames.Clear();
            personalList.Items.Clear();

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

            // Label de realizar corte de reporte semanal
            Label reportText = new Label();
            reportText.Text = "Realizar corte semanal";
            reportText.Font = new Font("Arial", 15, FontStyle.Regular);
            reportText.ForeColor = Color.Gray;
            reportText.Location = new Point(600, 100); // Posicionado a la derecha del label del empleado
            reportText.Size = new Size(300, 50);
            rightPanel.Controls.Add(reportText);

            // Botón para realizar el corte semanal
            Button weeklyReportButton = new Button();
            weeklyReportButton.Text = "Corte semanal";
            weeklyReportButton.Font = new Font("Arial", 12, FontStyle.Regular);
            weeklyReportButton.Location = new Point(650, 150); // Posicionado a la derecha del botón del empleado
            weeklyReportButton.Size = new Size(200, 40);
            weeklyReportButton.BackColor = Color.FromArgb(255, 204, 204);
            weeklyReportButton.ForeColor = Color.FromArgb(31, 30, 68);
            weeklyReportButton.FlatStyle = FlatStyle.Flat;
            weeklyReportButton.FlatAppearance.BorderSize = 0;
            weeklyReportButton.Click += new EventHandler(PerformWeeklyReport);
            rightPanel.Controls.Add(weeklyReportButton);

            // Icono para el botón de corte semanal
            IconPictureBox reportIcon = new IconPictureBox();
            reportIcon.IconChar = IconChar.FileInvoiceDollar;
            reportIcon.IconColor = Color.FromArgb(31, 30, 68);
            reportIcon.Location = new Point(600, 150); // Posicionado a la derecha del icono del empleado
            reportIcon.Size = new Size(32, 32);
            reportIcon.BackColor = Color.Transparent;
            rightPanel.Controls.Add(reportIcon);




            // Label de lista de empleados
            Label description2 = new Label();
            description2.Text = "Lista de empleados, Salario semanal";
            description2.Font = new Font("Arial", 15, FontStyle.Regular);
            description2.ForeColor = Color.Gray;
            description2.Location = new Point(300, 200);
            description2.Size = new Size(700, 50);
            rightPanel.Controls.Add(description2);

            // Crear tabla para mostrar los empleados
            employeeTable.Location = new Point(300, 250);
            employeeTable.Size = new Size(800, 200);
            employeeTable.BackgroundColor = Color.FromArgb(31, 30, 68);
            employeeTable.ForeColor = Color.Black;
            employeeTable.BorderStyle = BorderStyle.FixedSingle;
            employeeTable.ReadOnly = true;
            employeeTable.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            employeeTable.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            employeeTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 30, 68);
            employeeTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            employeeTable.EnableHeadersVisualStyles = false;
            employeeTable.AllowUserToAddRows = false; // Evitar mostrar una fila vacía por defecto

            if (employeeTable.Columns.Count == 0)
            {
                employeeTable.Columns.Add("ID", "ID");
                employeeTable.Columns.Add("Name", "Nombre");
                employeeTable.Columns.Add("WeeklySalary", "Salario Semanal");
                employeeTable.Columns.Add("Monday", "Lunes");
                employeeTable.Columns.Add("Tuesday", "Martes");
                employeeTable.Columns.Add("Wednesday", "Miércoles");
                employeeTable.Columns.Add("Thursday", "Jueves");
                employeeTable.Columns.Add("Friday", "Viernes");
                employeeTable.Columns.Add("Saturday", "Sábado");
                employeeTable.Columns.Add("Sunday", "Domingo");




                // Agregar columnas de Editar y Eliminar
                DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
                editColumn.Name = "Edit";
                editColumn.HeaderText = "Editar";
                editColumn.Text = "Editar";
                editColumn.UseColumnTextForButtonValue = true;
                employeeTable.Columns.Add(editColumn);

                DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
                deleteColumn.Name = "Delete";
                deleteColumn.HeaderText = "Eliminar";
                deleteColumn.Text = "Eliminar";
                deleteColumn.UseColumnTextForButtonValue = true;
                employeeTable.Columns.Add(deleteColumn);
            }

            // Desuscribirse de los eventos anteriores
            if (cellClickHandler != null)
            {
                employeeTable.CellClick -= cellClickHandler;
            }
            // Suscribir el evento CellFormatting
            employeeTable.CellFormatting += new DataGridViewCellFormattingEventHandler(EmployeeTable_CellFormatting);


            // Manejar eventos de clic en los botones de Editar y Eliminar
            cellClickHandler = new DataGridViewCellEventHandler((sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = employeeTable.Rows[e.RowIndex];

                    if (e.ColumnIndex == employeeTable.Columns["Edit"].Index)
                    {
                        // Editar el nombre del empleado
                        int id = Convert.ToInt32(row.Cells["ID"].Value);
                        string name = row.Cells["Name"].Value.ToString();
                        OpenEditPersonModal(id, name);
                    }
                    else if (e.ColumnIndex == employeeTable.Columns["Delete"].Index)
                    {
                        // Eliminar el registro del empleado
                        int id = Convert.ToInt32(row.Cells["ID"].Value);
                        DeleteEmployee(id);
                        FillEmployeeTable(employeeTable);
                    }
                }
            });

            // Suscribirse al evento
            employeeTable.CellClick += cellClickHandler;
            employeeTable.Rows.Clear();
            FillEmployeeTable(employeeTable);
            rightPanel.Controls.Add(employeeTable);


            // Label de reportes semanales
            Label reportDescription = new Label();
            reportDescription.Text = "Lista de reportes semanales";
            reportDescription.Font = new Font("Arial", 15, FontStyle.Regular);
            reportDescription.ForeColor = Color.Gray;
            reportDescription.Location = new Point(300, 470);
            reportDescription.Size = new Size(300, 50);
            rightPanel.Controls.Add(reportDescription);

            // Icono para el botón de corte semanal
            IconPictureBox filterReport = new IconPictureBox();
            filterReport.IconChar = IconChar.Search;
            filterReport.IconColor = Color.FromArgb(31, 30, 68);
            filterReport.Location = new Point(600, 470); // Posicionado a la derecha del icono del empleado
            filterReport.Size = new Size(32, 32);
            filterReport.BackColor = Color.Transparent;
            rightPanel.Controls.Add(filterReport);

            // Selector de fecha para filtrar los reportes por una fecha específica

            searchDatePicker.Location = new Point(650, 470);
            searchDatePicker.Size = new Size(100, 30);
            searchDatePicker.Format = DateTimePickerFormat.Short;
            searchDatePicker.Font = new Font("Arial", 12, FontStyle.Regular);

            // Evento para buscar cuando cambia la fecha en el DateTimePicker
            searchDatePicker.ValueChanged += new EventHandler((sender, e) =>
            {
                DateTime searchDate = searchDatePicker.Value;
                FillReportTable(searchDate);
            });

            rightPanel.Controls.Add(searchDatePicker);

            // Botón para realizar el corte semanal
            Button getAllReports = new Button();
            getAllReports.Text = "Traer todos los reportes";
            getAllReports.Font = new Font("Arial", 12, FontStyle.Regular);
            getAllReports.Location = new Point(880, 470); // Posicionado a la derecha del botón del empleado
            getAllReports.Size = new Size(200, 40);
            getAllReports.BackColor = Color.FromArgb(255, 204, 204);
            getAllReports.ForeColor = Color.FromArgb(31, 30, 68);
            getAllReports.FlatStyle = FlatStyle.Flat;
            getAllReports.FlatAppearance.BorderSize = 0;
            getAllReports.Click += new EventHandler(GetAllReports);
            rightPanel.Controls.Add(getAllReports);

            // Botón para realizar el corte semanal
            Button getFilterlReport = new Button();
            getFilterlReport.Text = "Filtrar";
            getFilterlReport.Font = new Font("Arial", 12, FontStyle.Regular);
            getFilterlReport.Location = new Point(780, 470); // Posicionado a la derecha del botón del empleado
            getFilterlReport.Size = new Size(80, 40);
            getFilterlReport.BackColor = Color.FromArgb(255, 204, 204);
            getFilterlReport.ForeColor = Color.FromArgb(31, 30, 68);
            getFilterlReport.FlatStyle = FlatStyle.Flat;
            getFilterlReport.FlatAppearance.BorderSize = 0;
            getFilterlReport.Click += new EventHandler(GetFillReportTable);
            rightPanel.Controls.Add(getFilterlReport);


            // Crear tabla para mostrar los reportes semanales
            reportTable.Location = new Point(300, 520);
            reportTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            reportTable.Size = new Size(800, 200);
            reportTable.BackgroundColor = Color.FromArgb(31, 30, 68);
            reportTable.ForeColor = Color.Black;
            reportTable.BorderStyle = BorderStyle.FixedSingle;
            reportTable.ReadOnly = true;
            reportTable.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            reportTable.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            reportTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 30, 68);
            reportTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            reportTable.EnableHeadersVisualStyles = false;
            reportTable.AllowUserToAddRows = false;

            // Configuración de columnas, solo si no han sido creadas previamente
            if (reportTable.Columns.Count == 0)
            {
                reportTable.Columns.Add("id", "ID");
                reportTable.Columns.Add("fechaInicio", "Fecha Inicio");
                reportTable.Columns.Add("fechaFin", "Fecha Fin");
                reportTable.Columns.Add("ingresoTotal", "Ingreso Total");
                reportTable.Columns.Add("manoDeObra", "Mano de Obra");
                reportTable.Columns.Add("salarios", "Salarios");
                reportTable.Columns.Add("ganancia", "Ganancia");
                reportTable.Columns.Add("refacciones", "Refacciones");

                // Agregar columna de botones para actualizar fechas
                DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn();
                updateButtonColumn.Name = "UpdateDates";
                updateButtonColumn.HeaderText = "Actualizar Fechas";
                updateButtonColumn.Text = "Actualizar";
                updateButtonColumn.UseColumnTextForButtonValue = true;
                reportTable.Columns.Add(updateButtonColumn);

                DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                deleteButtonColumn.Name = "DeleteDates";
                deleteButtonColumn.HeaderText = "Eliminar Reporte";
                deleteButtonColumn.Text = "Eliminar";
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                reportTable.Columns.Add(deleteButtonColumn);
            }

            // Manejador de eventos para la tabla de reportes
            DataGridViewCellEventHandler reportCellClickHandler = new DataGridViewCellEventHandler((sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == reportTable.Columns["UpdateDates"].Index)
                {
                    // Lógica para manejar la actualización de fechas
                    int reportId = Convert.ToInt32(reportTable.Rows[e.RowIndex].Cells["id"].Value);
                    DateTime fechaInicio = Convert.ToDateTime(reportTable.Rows[e.RowIndex].Cells["fechaInicio"].Value);
                    DateTime fechaFin = Convert.ToDateTime(reportTable.Rows[e.RowIndex].Cells["fechaFin"].Value);

                    OpenUpdateDate_Click(fechaInicio, fechaFin, reportId);

                }
                if (e.RowIndex >= 0 && e.ColumnIndex == reportTable.Columns["DeleteDates"].Index)
                {
                    // Lógica para manejar la actualización de fechas
                    int reportId = Convert.ToInt32(reportTable.Rows[e.RowIndex].Cells["id"].Value);

                    OpenDeleteDate_Click(reportId);

                }
            });

            // Desuscribirse de los eventos anteriores para evitar duplicaciones
            reportTable.CellClick -= reportCellClickHandler;

            // Suscribir el nuevo manejador de eventos
            reportTable.CellClick += reportCellClickHandler;

            // Formateo de celdas (si es necesario)
            reportTable.CellFormatting += new DataGridViewCellFormattingEventHandler(ReportTable_CellFormatting);


            // Limpiar las filas de la tabla antes de llenarla nuevamente
            reportTable.Rows.Clear();

            GetAllReportsFunction();

            // Agregar la tabla al panel (asegurarse de no agregarla repetidamente)
            if (!rightPanel.Controls.Contains(reportTable))
            {
                rightPanel.Controls.Add(reportTable);
            }
        }
        public void GetFillReportTable(object sender, EventArgs e)
        {
            FillReportTable(searchDatePicker.Value);

        }
        public void FillReportTable(DateTime searchDate)
        {
            try
            {
                DbConnect dbConnect = new DbConnect();
                string query = "SELECT id, fechaInicio, fechaFin, ingresoTotal, manoDeObra, salarios, ganancia, refacciones FROM report " +
                "WHERE DATE(fechaInicio) <= DATE(@searchDate) AND DATE(fechaFin) >= DATE(@searchDate)";


                using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
                {
                    //Limpiar la tabla antes de llenarla nuevamente
                    reportTable.Rows.Clear();
                    // Añadir el parámetro de búsqueda de fecha
                    cmd.Parameters.AddWithValue("@searchDate", searchDate);

                    dbConnect.OpenConnection();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        reportTable.Rows.Clear();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            DateTime fechaInicio = reader.GetDateTime("fechaInicio");
                            DateTime fechaFin = reader.GetDateTime("fechaFin");
                            decimal ingresoTotal = reader.GetDecimal("ingresoTotal");
                            decimal manoDeObra = reader.GetDecimal("manoDeObra");
                            decimal salarios = reader.GetDecimal("salarios");
                            decimal ganancia = reader.GetDecimal("ganancia");
                            decimal refacciones = reader.GetDecimal("refacciones");

                            reportTable.Rows.Add(id, fechaInicio.ToShortDateString(), fechaFin.ToShortDateString(), ingresoTotal, manoDeObra, salarios, ganancia, refacciones);
                        }
                    }
                    dbConnect.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar la tabla de reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            // Formateo de celdas (si es necesario)
            reportTable.CellFormatting += new DataGridViewCellFormattingEventHandler(ReportTable_CellFormatting);



            // Agregar la tabla al panel (asegurarse de no agregarla repetidamente)
            if (!rightPanel.Controls.Contains(reportTable))
            {
                rightPanel.Controls.Add(reportTable);
            }


        }
        public void GetAllReports(Object sender, EventArgs e)
        {

            GetAllReportsFunction();

        }
        public void GetAllReportsFunction()
        {
            try
            {
                // Inicializar la conexión a la base de datos
                DbConnect dbConnect = new DbConnect();
                string query = "SELECT * FROM report ORDER BY id DESC";
                DataTable dataTable = dbConnect.ExecuteQuery(query);

                // Limpiar la tabla antes de llenarla nuevamente
                reportTable.Rows.Clear();

                // Configurar la cultura para el formato de moneda
                var culture = new System.Globalization.CultureInfo("es-MX");

                foreach (DataRow row in dataTable.Rows)
                {
                    // Obtener y formatear los datos de la fila
                    int id = Convert.ToInt32(row["id"]);
                    string fechaInicio = Convert.ToDateTime(row["fechaInicio"]).ToString("dd/MM/yyyy");
                    string fechaFin = Convert.ToDateTime(row["fechaFin"]).ToString("dd/MM/yyyy");

                    decimal ingresoTotal = Convert.ToDecimal(row["ingresoTotal"]);
                    decimal manoDeObra = Convert.ToDecimal(row["manoDeObra"]);
                    decimal salarios = Convert.ToDecimal(row["salarios"]);
                    decimal ganancia = Convert.ToDecimal(row["ganancia"]);
                    decimal refacciones = Convert.ToDecimal(row["refacciones"]);

                    // Formatear los valores monetarios utilizando la cultura definida
                    string ingresoTotalFormateado = ingresoTotal.ToString("C", culture);
                    string manoDeObraFormateado = manoDeObra.ToString("C", culture);
                    string salariosFormateado = salarios.ToString("C", culture);
                    string gananciaFormateada = ganancia.ToString("C", culture);
                    string refaccionesFormateada = refacciones.ToString("C", culture);

                    // Agregar los datos formateados a la tabla
                    reportTable.Rows.Add(
                        id,
                        fechaInicio,
                        fechaFin,
                        ingresoTotalFormateado,
                        manoDeObraFormateado,
                        salariosFormateado,
                        gananciaFormateada,
                        refaccionesFormateada
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de los reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OpenDeleteDate_Click(int reportId)
        {
            // Preguntar si está seguro de eliminar el reporte
            DialogResult answer = MessageBox.Show("¿Está seguro de eliminar el reporte?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (answer == DialogResult.No)
            {
                return;
            }

            // Conectar a la base de datos y verificar si el reporte corresponde a la semana actual
            DbConnect dbConnect = new DbConnect();
            string checkCurrentWeekQuery = @"
                SELECT COUNT(*) 
                FROM report 
                WHERE id = @id 
                AND @fechaActual BETWEEN fechaInicio AND fechaFin";

            using (MySqlCommand cmd = new MySqlCommand(checkCurrentWeekQuery, dbConnect.Connection))
            {
                cmd.Parameters.AddWithValue("@id", reportId);
                cmd.Parameters.AddWithValue("@fechaActual", DateTime.Now.Date); // Fecha actual

                dbConnect.OpenConnection();
                int reportCount = Convert.ToInt32(cmd.ExecuteScalar());
                dbConnect.CloseConnection();

                if (reportCount > 0)
                {
                    MessageBox.Show("No se puede eliminar el reporte de la semana actual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Realizar la eliminación del reporte
            string query = "DELETE FROM report WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, dbConnect.Connection))
            {
                cmd.Parameters.AddWithValue("@id", reportId);

                dbConnect.OpenConnection();
                int result = cmd.ExecuteNonQuery();
                dbConnect.CloseConnection();

                if (result > 0)
                {
                    MessageBox.Show("El reporte ha sido eliminado correctamente.", "Reporte eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillReportTable(searchDatePicker.Value);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el reporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //Modificar las fechas en caso de que no coincidan
        private void ReportTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idReport = 0;
            // Verificar si se hizo clic en la columna de botones "Actualizar Fechas"
            if (e.ColumnIndex == reportTable.Columns["UpdateDates"].Index && e.RowIndex >= 0)
            {
                // Obtener el ID del reporte de la fila seleccionada
                // Obtener el valor de la celda "id" y asegurarse de que sea un número
                var cellValue = reportTable.Rows[e.RowIndex].Cells["id"].Value;

                if (cellValue != null && int.TryParse(cellValue.ToString(), out int reportId))
                {
                    idReport = reportId;

                }
                else
                {
                    MessageBox.Show("El valor de la columna 'id' no es un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Obtener las fechas actuales del reporte
                DateTime fechaInicioActual = Convert.ToDateTime(reportTable.Rows[e.RowIndex].Cells["fechaInicio"].Value);
                DateTime fechaFinActual = Convert.ToDateTime(reportTable.Rows[e.RowIndex].Cells["fechaFin"].Value);

                OpenUpdateDate_Click(fechaInicioActual, fechaFinActual, idReport);


            }
        }

        private void EmployeeTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la columna actual es una de las columnas de los días de la semana
            if (employeeTable.Columns[e.ColumnIndex].Name == "Monday" ||
                employeeTable.Columns[e.ColumnIndex].Name == "Tuesday" ||
                employeeTable.Columns[e.ColumnIndex].Name == "Wednesday" ||
                employeeTable.Columns[e.ColumnIndex].Name == "Thursday" ||
                employeeTable.Columns[e.ColumnIndex].Name == "Friday" ||
                employeeTable.Columns[e.ColumnIndex].Name == "Saturday" ||
                employeeTable.Columns[e.ColumnIndex].Name == "Sunday")

            {
                e.CellStyle.BackColor = Color.FromArgb(31, 39, 98); ;
                e.CellStyle.ForeColor = Color.White;
            }
        }
        private void ReportTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificar si la columna actual es una de las columnas de los días de la semana
            if (

                reportTable.Columns[e.ColumnIndex].Name == "ingresoTotal" ||
                reportTable.Columns[e.ColumnIndex].Name == "manoDeObra" ||
                reportTable.Columns[e.ColumnIndex].Name == "salarios" ||
                reportTable.Columns[e.ColumnIndex].Name == "ganancia" ||
                reportTable.Columns[e.ColumnIndex].Name == "refacciones")
            {
                e.CellStyle.BackColor = Color.FromArgb(31, 39, 98); ;
                e.CellStyle.ForeColor = Color.White;
            }
        }
        public void PerformWeeklyReport(object sender, EventArgs e)
        {
            // Preguntar si está seguro de realizar el corte semanal
            DialogResult result = MessageBox.Show("¿Está seguro de realizar el corte semanal y generar el nuevo reporte? Esta acción es válida únicamente los domingos. IMPORTANTE: ASEGURATE DE HABER CAPTURADO LAS COMISIONES DE LOS EMPLEADOS ¡SE LIMPIARÁ LA TABLA DE SALARIOS Y NO SERÁ REVERSIBLE! ", "Confirmar corte semanal", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                DbConnect dbConnect = new DbConnect();

                // Calcular la fecha de inicio y la fecha de fin
                DateTime fechaInicio = DateTime.Now.Date;
                if (fechaInicio.DayOfWeek == DayOfWeek.Sunday)
                {
                    fechaInicio = fechaInicio.AddDays(1); // Avanza al próximo lunes
                }
                DateTime fechaFin = fechaInicio.AddDays(6); // El domingo de la misma semana

                // Comprobar si ya existe un reporte para la semana actual o la siguiente semana
                string checkReportQuery = @"
                    SELECT COUNT(*) 
                    FROM report 
                    WHERE @fechaInicio BETWEEN fechaInicio AND fechaFin 
                    OR @fechaFin BETWEEN fechaInicio AND fechaFin";

                using (MySqlCommand cmd = new MySqlCommand(checkReportQuery, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                    dbConnect.OpenConnection();
                    int reportCount = Convert.ToInt32(cmd.ExecuteScalar());
                    dbConnect.CloseConnection();

                    if (reportCount > 0)
                    {
                        MessageBox.Show("Ya existe un reporte para esta semana o la siguiente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Establecer los valores iniciales de ingreso, salarios y ganancia a 0
                decimal ingresoTotal = 0;
                decimal salarios = 0;
                decimal ganancia = 0;
                decimal manoDeObra = 0;
                decimal refacciones = 0;

                // Insertar el nuevo registro en la tabla report con los valores iniciales en 0
                string insertReportQuery = "INSERT INTO report (fechaInicio, fechaFin, ingresoTotal, manoDeObra, salarios, ganancia, refacciones) VALUES (@fechaInicio, @fechaFin, @ingresoTotal, @salarios, @manoDeObra, @ganancia, @refacciones)";

                using (MySqlCommand cmd = new MySqlCommand(insertReportQuery, dbConnect.Connection))
                {
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@ingresoTotal", ingresoTotal);
                    cmd.Parameters.AddWithValue("@salarios", salarios);
                    cmd.Parameters.AddWithValue("@ganancia", ganancia);
                    cmd.Parameters.AddWithValue("@manoDeObra", manoDeObra);
                    cmd.Parameters.AddWithValue("@refacciones", refacciones);

                    dbConnect.OpenConnection();
                    cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();
                }

                // Resetear los salarios en la tabla person a 0.00
                string resetSalariesQuery = "UPDATE person SET salario = 0.00, lunes = 0, martes = 0, miercoles = 0, jueves = 0, viernes = 0, sabado = 0, domingo = 0";

                using (MySqlCommand cmd = new MySqlCommand(resetSalariesQuery, dbConnect.Connection))
                {
                    dbConnect.OpenConnection();
                    cmd.ExecuteNonQuery();
                    dbConnect.CloseConnection();
                }

                // Actualizar la tabla de reportes
                GetAllReportsFunction();
                // Actualizar la tabla de empleados
                FillEmployeeTable(employeeTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar el corte semanal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void ResetSalariesIfMonday()
        {
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Monday && now.Hour == 11 && now.Minute == 51)
            {
                try
                {
                    DbConnect dbConnect = new DbConnect();

                    // Calcular la fecha de inicio y la fecha de fin
                    DateTime fechaInicio = now.Date;
                    DateTime fechaFin = fechaInicio.AddDays(6); // El domingo de la misma semana

                    // Establecer los valores iniciales de ingreso, salarios y ganancia a 0
                    decimal ingresoTotal = 0;
                    decimal salarios = 0;
                    decimal ganancia = 0;

                    // Insertar el nuevo registro en la tabla report con los valores iniciales en 0
                    string insertReportQuery = "INSERT INTO report (fechaInicio, fechaFin, ingresoTotal, salarios, ganancia) VALUES (@fechaInicio, @fechaFin, @ingresoTotal, @salarios, @ganancia)";

                    using (MySqlCommand cmd = new MySqlCommand(insertReportQuery, dbConnect.Connection))
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                        cmd.Parameters.AddWithValue("@ingresoTotal", ingresoTotal);
                        cmd.Parameters.AddWithValue("@salarios", salarios);
                        cmd.Parameters.AddWithValue("@ganancia", ganancia);

                        dbConnect.OpenConnection();
                        cmd.ExecuteNonQuery();
                        dbConnect.CloseConnection();
                    }

                    // Resetear los salarios en la tabla person a 0.00
                    string resetSalariesQuery = "UPDATE person SET salario = 0.00";

                    using (MySqlCommand cmd = new MySqlCommand(resetSalariesQuery, dbConnect.Connection))
                    {
                        dbConnect.OpenConnection();
                        cmd.ExecuteNonQuery();
                        dbConnect.CloseConnection();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al resetear los salarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void FillEmployeeTable(DataGridView employeeTable)
        {
            try
            {
                // Inicializar la conexión a la base de datos
                DbConnect dbConnect = new DbConnect();
                string query = "SELECT id, nombre, salario, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo FROM person";
                DataTable dataTable = dbConnect.ExecuteQuery(query);

                // Limpiar la tabla antes de llenarla nuevamente
                employeeTable.Rows.Clear();

                // Configurar la cultura para el formato de moneda
                var culture = new System.Globalization.CultureInfo("es-MX");

                foreach (DataRow row in dataTable.Rows)
                {
                    // Obtener los datos de la fila
                    int id = Convert.ToInt32(row["id"]);
                    string nombre = row["nombre"].ToString();

                    decimal lunes = Convert.ToDecimal(row["Lunes"]);
                    decimal martes = Convert.ToDecimal(row["Martes"]);
                    decimal miercoles = Convert.ToDecimal(row["Miercoles"]);
                    decimal jueves = Convert.ToDecimal(row["Jueves"]);
                    decimal viernes = Convert.ToDecimal(row["Viernes"]);
                    decimal sabado = Convert.ToDecimal(row["Sabado"]);
                    decimal domingo = Convert.ToDecimal(row["Domingo"]);

                    // Calcular el salario total sumando los valores de todos los días
                    decimal salarioTotal = lunes + martes + miercoles + jueves + viernes + sabado + domingo;

                    // Actualizar el salario total en la base de datos
                    string updateSalaryQuery = "UPDATE person SET salario = @salarioTotal WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(updateSalaryQuery, dbConnect.Connection))
                    {
                        cmd.Parameters.AddWithValue("@salarioTotal", salarioTotal);
                        cmd.Parameters.AddWithValue("@id", id);

                        dbConnect.OpenConnection();
                        cmd.ExecuteNonQuery();
                        dbConnect.CloseConnection();
                    }

                    // Agregar los datos a la tabla, formateando los valores como moneda
                    employeeTable.Rows.Add(
                        id,
                        nombre,
                        salarioTotal.ToString("C", culture),
                        lunes.ToString("C", culture),
                        martes.ToString("C", culture),
                        miercoles.ToString("C", culture),
                        jueves.ToString("C", culture),
                        viernes.ToString("C", culture),
                        sabado.ToString("C", culture),
                        domingo.ToString("C", culture)
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos de los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Modales y operaciones terceros
        public void OpenEditPersonModal(int id, string name)
        {
            using (EditPerson editPersonModal = new EditPerson(this, id, name))
            {
                editPersonModal.ShowDialog(Form1.ActiveForm);
                FillEmployeeTable(employeeTable);
            }



        }
        private void OpenCreateANewPerson(object sender, EventArgs e)
        {
            using (CreateANewPersonModal createANewPerson = new CreateANewPersonModal(this))
            {
                createANewPerson.ShowDialog(Form1.ActiveForm);

                // Actualizar la tabla de empleados después de cerrar el modal
                FillEmployeeTable(employeeTable);
            }
        }
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

                        // Truncate customers table
                        string queryCustomers = "TRUNCATE TABLE customers";
                        dbConnect.ExecuteQuery(queryCustomers);

                        // Truncate person table
                        string queryPerson = "TRUNCATE TABLE person";
                        dbConnect.ExecuteQuery(queryPerson);

                        // Truncate report table
                        string queryReport = "TRUNCATE TABLE report";
                        dbConnect.ExecuteQuery(queryReport);

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
                    saveFileDialog.FileName = "backup_tekno_" + DateTime.Now.ToString("yyyy_MM_dd") + ".sql";

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
                        string mysqldumpPath = @"C:\Program Files\MariaDB 11.4\bin\mysqldump.exe"; // Ajusta esta ruta según tu instalación

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
        public void GetPersonalNames()
        {
            personalNames.Clear(); // Limpiar la lista antes de agregar nuevos nombres

            // Instanciar la clase DbConnect y ejecutar la consulta
            DbConnect dbConnect = new DbConnect();
            string query = "SELECT nombre FROM person";
            DataTable dataTable = dbConnect.ExecuteQuery(query);

            // Agregar los nombres de los empleados a la lista personalNames
            foreach (DataRow row in dataTable.Rows)
            {
                string name = row["nombre"].ToString();
                personalNames.Add(name);
            }
            // Actualizar la lista de empleados
            personalList.Items.Clear();
            personalList.Items.AddRange(personalNames.ToArray());

        }
        private void FilterButton_Click(object sender, EventArgs e)
        {
            string selectedFilter = filter.SelectedItem?.ToString()!;

            if (string.IsNullOrEmpty(selectedFilter))
            {
                MessageBox.Show("Por favor, selecciona un filtro.");
                return;
            }


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
            using (CustomerForm customerForm = new CustomerForm(this))
            {
                customerForm.ShowDialog(Form1.ActiveForm);
            }
        }
        private void OpenAtentionFormButton_Click(object sender, EventArgs e, int id, string name, string tipoDispositivo, string brand, string model, string problem, string statusNow, string fechaEntregar)
        {
            using (RepairDeviceModal customerForm = new RepairDeviceModal(this, id: id, name: name, tipoDispositivo: tipoDispositivo, brand: brand, model: model, problem: problem, statusNow: statusNow, fechaEntregar: fechaEntregar))
            {
                customerForm.ShowDialog(Form1.ActiveForm);
            }
        }
        private void OpenConfirmDeliverdFinished_Click(object sender, EventArgs e, int id, string name, string tipoDispositivo, string brand, string model, string problem, string statusNow, string fechaReparado, string costo, string diagnostico, string personaReparo, string personaRecibio, string fechaRecibido)
        {
            using (ConfirmFinishDelivered customerForm = new ConfirmFinishDelivered(this, id: id, name: name, tipo_dispositivo: tipoDispositivo, brand: brand, model: model, problem: problem, status: statusNow, fechaReparado: fechaReparado, costo: costo, diagnostico: diagnostico, personaReparo: personaReparo, personaRecibio: personaRecibio, fechaRecibido: fechaRecibido))
            {
                customerForm.ShowDialog(Form1.ActiveForm);
            }
        }
        private void OpenDeliveredModalButton_Click(object sender, EventArgs e, int id, string name, string model, string brand, string status, string problem, string fechaRecibido, string fechaEntregar)
        {
            using (DeliveredToLaboratoryModal deliveredModal = new DeliveredToLaboratoryModal(this, id, name, brand, model, status, problem, fechaRecibido, fechaEntregar))
            {
                deliveredModal.ShowDialog(Form1.ActiveForm);
            }
        }


        private void DeleteRecordById(int id)
        {

            // Preguntar al usuario si está seguro de eliminar el registro
            DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Instanciar la clase DbConnect y ejecutar la consulta de eliminación
                DbConnect dbConnect = new DbConnect();
                string query = $"DELETE FROM customers WHERE id = {id}";
                dbConnect.ExecuteQuery(query);

                // Actualizar el DataGridView después de eliminar el registro
                GetFilterRegisters(filter.Text, search.Text);

                MessageBox.Show("Se eliminó correctamente el cliente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void ConfirmDelivered(int id)
        {
            DialogResult result = MessageBox.Show("Marcaste a este dispositivo como entregado", "Confirmar entrega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                DbConnect dbConnect = new DbConnect();

                string query = $"UPDATE customers SET estatus = 'ENTREGADO' WHERE id = {id}";
                dbConnect.ExecuteQuery(query);

                GetFilterRegisters(filter.Text, search.Text);

                MessageBox.Show("El dispositivo se entregó", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void OpenEditCustomerModal(string name, string phone, string brand, string model, string reason, string date, string hour, string status)
        {
            using (EditCustomer editCustomerModal = new EditCustomer(this, name, phone, brand, model, reason, date, hour, status))
            {
                editCustomerModal.ShowDialog(Form1.ActiveForm);
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Llamar al método que deseas ejecutar cada minuto
            ExecutePeriodicTask();
            GetFilterRegisters(filter.Text, search.Text);
            if (isEmployeeSectionActive)
            {
                FillEmployeeTable(employeeTable);
                // GetAllReportsFunction();
            }

        }
        private void ExecutePeriodicTask()
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