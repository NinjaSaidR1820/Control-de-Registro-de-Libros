using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_de_Registro_de_Libros
{
    public partial class frmLibros : Form
    {

        static int contador;
        public frmLibros()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            lblNumero.Text = generaNumero();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Valida() == "")
            {
                double costo = getCosto();
                string categoria = getCategoria();
                double descuento = asignaDescuento(categoria, costo);
                double precioVenta = calcularPrecioVenta(costo, descuento);

                imprimirRegistro(descuento, precioVenta);
                lblNumero.Text = generaNumero();

            }
            else MessageBox.Show("PorFavor Ingresar: " + Valida());
        }

        Func<string> generaNumero = () =>
        {
            contador++;
            return contador.ToString("0000");
        };

        int getNumero()
        {
            return int.Parse(lblNumero.Text);
        }
        string gettitulo()
        {
            return txtTitulo.Text;
        }
        double getCosto()
        {
            return double.Parse(txtCosto.Text);
        }

        string getCategoria()
        {
            return cbCategoria.Text;
        }

        Func<string, double, double> asignaDescuento = (categoria, costo) =>
        {
            double descuento = 0;
            switch (categoria)
            {
                case "Gestion":  descuento = 10.0 / 100 * costo;  break;

                case "Ingenieria": descuento = 12.0 / 100 * costo; break;
                case "Programacion": descuento = 20.0 / 100 * costo; break;

                case "Base de Datos": descuento = 15.0 / 100 * costo; break;
            }
            return descuento;

        };

        Func<double, double, double> calcularPrecioVenta = (costo, descuento) => costo - descuento;

        double calculaTotalDescuentos()
        {
            double total = 0;
            for(int i = 0; i < lvLibros.Items.Count; i++)
            {
                total += double.Parse(lvLibros.Items[i].SubItems[4].Text);
            }
            return total;
        }

        string LibroMasAlto()
        {
            double mayor = double.Parse(lvLibros.Items[0].SubItems[5].Text);
            int posicion = 0;

            for (int i = 0; i < lvLibros.Items.Count; i++)
            {
                if(double.Parse(lvLibros.Items[i].SubItems[5].Text) > mayor){
                    posicion = i;
                }
               
            }
            return lvLibros.Items[posicion].SubItems[1].Text;
        }

        void imprimirRegistro(double descuento, double precioVenta)
        {
            ListViewItem fila = new ListViewItem(getNumero().ToString());

            fila.SubItems.Add(gettitulo());
            fila.SubItems.Add(getCategoria());
            fila.SubItems.Add(getCosto().ToString("0.00"));
            fila.SubItems.Add(descuento.ToString("0.00"));
            fila.SubItems.Add(precioVenta.ToString("0.00"));
            
            lvLibros.Items.Add(fila);
        }

        void imprimirEstadisticas(double totalDescuento, string LibroAlto)
        {
            lvEstadistica.Items.Clear();

            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Monto total acumulado de descuento";
            elementosFila[1] = totalDescuento.ToString("C");

            row = new ListViewItem(elementosFila);

            lvEstadistica.Items.Add(row);


            elementosFila[0] = "El Libro con el Precio de venta mas Caro";
            elementosFila[1] = LibroAlto;
            row = new ListViewItem(elementosFila);
            lvEstadistica.Items.Add(row);
        }

        string Valida()
        {
            if (txtTitulo.Text.Trim().Length == 0)
            {
                txtTitulo.Focus();
                return "Titulo del Libro";

            } else if (cbCategoria.SelectedIndex == -1)
            {
                cbCategoria.Focus();
                return "Categoria del Libro";

            } else if (txtCosto.Text.Trim().Length == 0)
            {
                txtCosto.Focus();
                return "Costo del libro";

            }
            return "";
        }

        private void btnEstadistica_Click(object sender, EventArgs e)
        {

            double totalDescuento = calculaTotalDescuentos();
            string libroAlto = LibroMasAlto();

            imprimirEstadisticas(totalDescuento, libroAlto);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            lvLibros.Items.Clear();
            lvEstadistica.Items.Clear();
            txtTitulo.Clear();
            txtCosto.Text = "";
           
            cbCategoria.Text = "";
        }
    }
}
