using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Common;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace Polyanka_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PolyankaEntities db = new PolyankaEntities();
        DataTable tdata;
        SqlConnection conn;
        SqlDataAdapter dataAd;
        string selectedTable;
        string selectedColumn;
        static string pop;
        List<string> list = new List<string> { "Supplies", "Delivery", "Storehouse", "Posts", "Suppliers", "Products_in_storehouse", "Menu", "Order_", "Employees", "Customers", "Order_point", "Users" };
        int count = 0;
        private ICollectionView defaultView;
        public MainWindow()
        {
            InitializeComponent();
            Menu.ItemsSource = list;
            Menu.SelectedIndex = 0;
            selectedTable = (string)Menu.SelectedValue;
            Func(selectedTable);
            string connectionString = "Server=KIRILL;Database=Polyanka;Trusted_Connection=True;";
            conn = new SqlConnection(connectionString);
            LoadTableData();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new PolyankaEntities();

        }

        private void Func(string str)
        {

            using (SqlConnection conect = new SqlConnection(@"Server=KIRILL;Integrated Security=SSPI;Database=Polyanka"))
            {
                string zapros = $"SELECT * FROM {str}";
                using (SqlCommand cmd = new SqlCommand(zapros, conect))
                {
                    DataTable tab = new DataTable($"{str}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conect.Open();
                    adapter.Fill(tab);
                    showTable.ItemsSource = tab.DefaultView;
                    conect.Close();
                }
            }

        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)showTable.SelectedItem;
            if (selectedTable == "Supplies")
            {
                Supplies sup = new Supplies();
                sup.id_supplier = (int)dataRow.Row.ItemArray[1];
                sup.product_name = dataRow.Row.ItemArray[2].ToString();
                sup.unit = dataRow.Row.ItemArray[3].ToString();
                sup.quantity = (int)dataRow.Row.ItemArray[4];
                db.Supplies.Add(sup);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Delivery")
            {
                Delivery del = new Delivery();
                del.id_supplies = (int)dataRow.Row.ItemArray[1];
                del.id_storehouse = (int)dataRow.Row.ItemArray[2];
                del.date_of_delivery = Convert.ToDateTime(dataRow.Row.ItemArray[3]);
                del.full_cost = (int)dataRow.Row.ItemArray[4];
                db.Delivery.Add(del);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Storehouse")
            {
                Storehouse st = new Storehouse();
                st.name_ = dataRow.Row.ItemArray[1].ToString();
                st.location_ = dataRow.Row.ItemArray[2].ToString();
                db.Storehouse.Add(st);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Posts")
            {
                Posts post = new Posts();
                post.name_of_post = dataRow.Row.ItemArray[1].ToString();
                post.description_ = dataRow.Row.ItemArray[2].ToString();
                db.Posts.Add(post);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Suppliers")
            {
                Suppliers sup = new Suppliers();
                sup.name_ = dataRow.Row.ItemArray[1].ToString();
                sup.contact_info = dataRow.Row.ItemArray[2].ToString();
                db.Suppliers.Add(sup);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Products_in_storehouse")
            {
                Products_in_storehouse pInSt = new Products_in_storehouse();
                pInSt.id_Storehouse = (int)dataRow.Row.ItemArray[1];
                pInSt.product_name = dataRow.Row.ItemArray[2].ToString();
                pInSt.unit = dataRow.Row.ItemArray[3].ToString();
                pInSt.quantity = (int)dataRow.Row.ItemArray[4];
                db.Products_in_storehouse.Add(pInSt);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Menu")
            {
                Menu menu = new Menu();
                menu.dish_name = dataRow.Row.ItemArray[1].ToString();
                menu.description_ = dataRow.Row.ItemArray[2].ToString();
                menu.price = (int)dataRow.Row.ItemArray[3];
                db.Menu.Add(menu);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Order_")
            {
                Order_ order = new Order_();
                order.id = (int)dataRow.Row.ItemArray[1];
                order.date_of_order = Convert.ToDateTime(dataRow.Row.ItemArray[2]);
                order.cost_of_order = (int)dataRow.Row.ItemArray[3];
                db.Order_.Add(order);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Employees")
            {
                Employees emp = new Employees();
                emp.id_storehouse = (int)dataRow.Row.ItemArray[1];
                emp.id_order = (int)dataRow.Row.ItemArray[2];
                emp.FIO = dataRow.Row.ItemArray[3].ToString();
                emp.post = (int)dataRow.Row.ItemArray[4];
                emp.gender = dataRow.Row.ItemArray[5].ToString();
                db.Employees.Add(emp);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Customers")
            {
                Customers cust = new Customers();
                cust.FIO = dataRow.Row.ItemArray[1].ToString();
                cust.contact_info = dataRow.Row.ItemArray[2].ToString();
                db.Customers.Add(cust);
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Order_point")
            {
                Order_point ordPoint = new Order_point();
                ordPoint.id_order = (int)dataRow.Row.ItemArray[1];
                ordPoint.id_dish = (int)dataRow.Row.ItemArray[2];
                ordPoint.quantity = (int)dataRow.Row.ItemArray[3];
                db.Order_point.Add(ordPoint);
                db.SaveChanges();
                Func(selectedTable);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)showTable.SelectedItem;
            int anyId = (int)dataRow.Row.ItemArray[0];
            if (selectedTable == "Supplies")
            {
                var sup = db.Supplies.Where(w => w.id == anyId).FirstOrDefault();
                sup.id_supplier = (int)dataRow.Row.ItemArray[1];
                sup.product_name = dataRow.Row.ItemArray[2].ToString();
                sup.unit = dataRow.Row.ItemArray[3].ToString();
                sup.quantity = (int)dataRow.Row.ItemArray[4];
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Delivery")
            {
                var del = db.Delivery.Where(w => w.id == anyId).FirstOrDefault();
                del.id_supplies = (int)dataRow.Row.ItemArray[1];
                del.id_storehouse = (int)dataRow.Row.ItemArray[2];
                del.date_of_delivery = Convert.ToDateTime(dataRow.Row.ItemArray[3]);
                del.full_cost = (int)dataRow.Row.ItemArray[4];
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Storehouse")
            {
                var st = db.Storehouse.Where(w => w.id == anyId).FirstOrDefault();
                st.name_ = dataRow.Row.ItemArray[1].ToString();
                st.location_ = dataRow.Row.ItemArray[2].ToString();
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Posts")
            {
                var post = db.Posts.Where(w => w.id == anyId).FirstOrDefault();
                post.name_of_post = dataRow.Row.ItemArray[1].ToString();
                post.description_ = dataRow.Row.ItemArray[2].ToString();
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Suppliers")
            {
                var sup = db.Suppliers.Where(w => w.id == anyId).FirstOrDefault();
                sup.name_ = dataRow.Row.ItemArray[1].ToString();
                sup.contact_info = dataRow.Row.ItemArray[2].ToString();
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Products_in_storehouse")
            {
                var pInSt = db.Products_in_storehouse.Where(w => w.id == anyId).FirstOrDefault();
                pInSt.id_Storehouse = (int)dataRow.Row.ItemArray[1];
                pInSt.product_name = dataRow.Row.ItemArray[2].ToString();
                pInSt.unit = dataRow.Row.ItemArray[3].ToString();
                pInSt.quantity = (int)dataRow.Row.ItemArray[4];
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Menu")
            {
                var menu = db.Menu.Where(w => w.id == anyId).FirstOrDefault();
                menu.dish_name = dataRow.Row.ItemArray[1].ToString();
                menu.description_ = dataRow.Row.ItemArray[2].ToString();
                menu.price = (int)dataRow.Row.ItemArray[3];
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Order_")
            {
                var order = db.Order_.Where(w => w.id == anyId).FirstOrDefault();
                order.id = (int)dataRow.Row.ItemArray[1];
                order.date_of_order = Convert.ToDateTime(dataRow.Row.ItemArray[2]);
                order.cost_of_order = (int)dataRow.Row.ItemArray[3];
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Employees")
            {
                var emp = db.Employees.Where(w => w.id == anyId).FirstOrDefault();
                emp.id_storehouse = (int)dataRow.Row.ItemArray[1];
                emp.id_order = (int)dataRow.Row.ItemArray[2];
                emp.FIO = dataRow.Row.ItemArray[3].ToString();
                emp.post = (int)dataRow.Row.ItemArray[4];
                emp.gender = dataRow.Row.ItemArray[5].ToString();
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Customers")
            {
                var cust = db.Customers.Where(w => w.id == anyId).FirstOrDefault();
                cust.FIO = dataRow.Row.ItemArray[1].ToString();
                cust.contact_info = dataRow.Row.ItemArray[2].ToString();
                db.SaveChanges();
                Func(selectedTable);
            }
            else if (selectedTable == "Order_point")
            {
                var ordPoint = db.Order_point.Where(w => w.id == anyId).FirstOrDefault();
                ordPoint.id_order = (int)dataRow.Row.ItemArray[1];
                ordPoint.id_dish = (int)dataRow.Row.ItemArray[2];
                ordPoint.quantity = (int)dataRow.Row.ItemArray[3];
                db.SaveChanges();
                Func(selectedTable);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (showTable.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)showTable.SelectedItem;
                int anyId = (int)dataRow.Row.ItemArray[0];
                if (selectedTable == "Supplies")
                {
                    var sup = db.Supplies.Where(w => w.id == anyId).FirstOrDefault();
                    db.Supplies.Remove(sup);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Delivery")
                {
                    var del = db.Delivery.Where(w => w.id == anyId).FirstOrDefault();
                    db.Delivery.Remove(del);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Storehouse")
                {
                    var st = db.Storehouse.Where(w => w.id == anyId).FirstOrDefault();
                    db.Storehouse.Remove(st);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Posts")
                {
                    var post = db.Posts.Where(w => w.id == anyId).FirstOrDefault();
                    db.Posts.Remove(post);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Suppliers")
                {
                    var sup = db.Suppliers.Where(w => w.id == anyId).FirstOrDefault();
                    db.Suppliers.Remove(sup);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Products_in_storehouse")
                {
                    var pInSt = db.Products_in_storehouse.Where(w => w.id == anyId).FirstOrDefault();
                    db.Products_in_storehouse.Remove(pInSt);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Menu")
                {
                    var menu = db.Menu.Where(w => w.id == anyId).FirstOrDefault();
                    db.Menu.Remove(menu);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Order_")
                {
                    var order = db.Order_.Where(w => w.id == anyId).FirstOrDefault();
                    db.Order_.Remove(order);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Employees")
                {
                    var emp = db.Employees.Where(w => w.id == anyId).FirstOrDefault();
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Customers")
                {
                    var cust = db.Customers.Where(w => w.id == anyId).FirstOrDefault();
                    db.Customers.Remove(cust);
                    db.SaveChanges();
                    Func(selectedTable);
                }
                else if (selectedTable == "Order_point")
                {
                    var ordPoint = db.Order_point.Where(w => w.id == anyId).FirstOrDefault();
                    db.Order_point.Remove(ordPoint);
                    db.SaveChanges();
                    Func(selectedTable);
                }
            }
        }

        private void textN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                conn.Open();
                dataAd = new SqlDataAdapter($"SELECT * FROM {selectedTable} where FIO like '{textN.Text}%'", conn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAd);
                tdata = new DataTable();
                dataAd.Fill(tdata);
                showTable.ItemsSource = tdata.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Menu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            pop = Menu.SelectedItem.ToString();
            Func(pop);

            selectedTable = (string)Menu.SelectedValue;
        }

        private void SaveChanges()
        {
            try
            {
                conn.Open();
                dataAd.Update(tdata);
                MessageBox.Show("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void LoadTableData()
        {
            try
            {
                conn.Open();
                dataAd = new SqlDataAdapter($"SELECT * FROM {selectedTable}", conn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAd);
                tdata = new DataTable();
                dataAd.Fill(tdata);
                showTable.ItemsSource = tdata.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Report_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog pD = new PrintDialog();
                if (pD.ShowDialog() == true)
                {
                    pD.PrintVisual(showTable, "Ура");
                }

            }
            finally
            {
                this.IsEnabled = true;

            }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)showTable.SelectedItem;
            int selectedRow = (int)dataRow.Row.ItemArray[0];
            string fileName;
            var selectIn = db.Users.Where(w => w.id == selectedRow).FirstOrDefault();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string sourceFilePath = openFileDialog.FileName;

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolder = System.IO.Path.Combine(path, "Images");

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(sourceFilePath);
                string destinationFilePath = System.IO.Path.Combine(imagesFolder, fileName);

                File.Copy(sourceFilePath, destinationFilePath, false);
                byte[] imageBytes = File.ReadAllBytes(destinationFilePath);

                selectIn.img = imageBytes;

                db.SaveChanges();
                Func(selectedTable);
            }
        }
    }


}
