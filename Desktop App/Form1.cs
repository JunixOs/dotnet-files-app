using Desktop_App.Api;
using Desktop_App.Models;

namespace Desktop_App
{
    public partial class Form1 : Form
    {
        private Label TitleLabel { get; set; } = null!;
        private Button SearchFilmButton { get; set; } = null!;
        private DataGridView TableForFilms { get; set; } = null!;

        public Form1()
        {
            InitializeComponent(); // Esto siempre va primero
            // Luego colocas tu logica de widgets
            ConfigureForm();
            CreateFormWidgets();
            ConfigureFormWidgets();
            DisplayWidgets();
        }

        private void ConfigureForm()
        {
            this.Text = "Desktop App"; // Modificar el titulo de la ventana
            this.StartPosition = FormStartPosition.CenterScreen; // Mover la ventana al centro de la pantalla
            this.Size = new Size(800, 600); // Definimos el tamaño de la ventana
        }

        private void CreateFormWidgets()
        {
            TitleLabel = new Label();
            SearchFilmButton = new Button();
            TableForFilms = new DataGridView();
        }

        private void ConfigureFormWidgets()
        {
            // Label
            TitleLabel.Text = "Films App";
            TitleLabel.Location = new Point(100, 10);
            TitleLabel.Size = new Size(600, 30);
            TitleLabel.Font = new Font("Times New Roman", 18, FontStyle.Bold);

            // Button
            SearchFilmButton.Text = "Añadir pelicula"; // Añadir texto
            SearchFilmButton.Location = new Point(450, 50); // Ubicar boton
            SearchFilmButton.Size = new Size(100, 30);

            SearchFilmButton.BackColor = Color.Purple; // Color de fondo
            SearchFilmButton.ForeColor = Color.White; // Color de Fuente

            SearchFilmButton.Font = new Font("Times New Roman", 12, FontStyle.Bold); // Tipo de Fuente y Tamaño
            SearchFilmButton.FlatStyle = FlatStyle.Flat; // Bordes del boton
        
            // DataGridView
            TableForFilms.Location = new Point(20 , 100);
            TableForFilms.Size = new Size(740 , 300);

            TableForFilms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Especificamos que las columnas se ajusten al ancho
            TableForFilms.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Especificamos que al seleccionar una celda se seleccione una fila
            TableForFilms.ReadOnly = true; // Lo hacemos solo de lectura
        }

        private void DisplayWidgets()
        {
            this.Controls.Add(TitleLabel);
            this.Controls.Add(SearchFilmButton); // Siempre añade el widget al final
            this.Controls.Add(TableForFilms);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            FilmsApi filmsApi = new FilmsApi();

            ResultModel<List<FilmModel>> result = await filmsApi.GetAllFilms();

            if (!result.Success)
            {
                MessageBox.Show(
                    $"Ocurrio un error al solicitar la información: {result.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return ;
            }

            TableForFilms.DataSource = result.Data;
        }
    }
}
