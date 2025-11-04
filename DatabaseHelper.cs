using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace AnimalFeedApp.Helpers
{
    internal static class DatabaseHelper
    {
        private static string dbFile = "AnimalFeed.db"; // القيمة الافتراضية
        private static string connectionString => $"Data Source={dbFile};Version=3;";

        static DatabaseHelper()
        {
            LoadSettingsPath(); // تحميل المسار عند تشغيل التطبيق
        }

        // ✅ تحميل مسار قاعدة البيانات من ملف الإعدادات
        private static void LoadSettingsPath()
        {
            try
            {
                string settingsFile = "appsettings.txt";
                if (File.Exists(settingsFile))
                {
                    foreach (var line in File.ReadAllLines(settingsFile))
                    {
                        if (line.StartsWith("DB_PATH="))
                        {
                            string path = line.Substring(8).Trim();
                            if (File.Exists(path))
                                dbFile = path; // استخدم المسار من الإعدادات
                            else
                                MessageBox.Show($"⚠️ لم يتم العثور على قاعدة البيانات في المسار:\n{path}\nسيتم إنشاء واحدة جديدة.", "تنبيه");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ خطأ أثناء قراءة إعدادات قاعدة البيانات:\n" + ex.Message);
            }
        }

        /*************************************************/

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string createTables = @"
                        CREATE TABLE IF NOT EXISTS Inventory (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ItemName TEXT NOT NULL,
                            Quantity REAL NOT NULL,
                            PricePerUnit REAL,
                            DateAdded TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Sales (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            CustomerName TEXT,
                            ItemName TEXT,
                            Quantity REAL,
                            TotalPrice REAL,
                            SaleDate TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Purchases (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            SupplierName TEXT,
                            ItemName TEXT,
                            Quantity REAL,
                            TotalCost REAL,
                            PurchaseDate TEXT
                        );
                    ";
                    using (var cmd = new SQLiteCommand(createTables, conn))
                        cmd.ExecuteNonQuery();
                }
            }
        }

        /*************************************************/

        public static DataTable GetDataTable(string query)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                using (var adapter = new SQLiteDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /*************************************************/

        public static void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }

        /*************************************************/

        public static object ExecuteScalar(string query)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                return cmd.ExecuteScalar();
            }
        }

        /*******************************************************/

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }

    /*******************************************************
     * تقارير المبيعات والمشتريات والمخزون
     *******************************************************/
    public static class Reports
    {
        public static DataTable GetSalesReport(string type)
        {
            string query = BuildDateQuery("Sales", "SaleDate", type);
            return DatabaseHelper.GetDataTable(query);
        }

        public static DataTable GetPurchasesReport(string type)
        {
            string query = BuildDateQuery("Purchases", "PurchaseDate", type);
            return DatabaseHelper.GetDataTable(query);
        }

        public static DataTable GetInventoryReport()
        {
            string query = "SELECT ItemName AS 'اسم المنتج', Quantity AS 'الكمية المتوفرة', PricePerUnit AS 'السعر للوحدة', DateAdded AS 'تاريخ الإضافة' FROM Inventory";
            return DatabaseHelper.GetDataTable(query);
        }

        // تبني استعلام التاريخ حسب نوع التقرير
        private static string BuildDateQuery(string table, string dateColumn, string type)
        {
            string baseQuery = $"SELECT * FROM {table} WHERE 1=1 ";

            switch (type)
            {
                case "يومي":
                    baseQuery += $"AND date({dateColumn}) = date('now')";
                    break;
                case "شهري":
                    baseQuery += $"AND strftime('%Y-%m', {dateColumn}) = strftime('%Y-%m', 'now')";
                    break;
                case "سنوي":
                    baseQuery += $"AND strftime('%Y', {dateColumn}) = strftime('%Y', 'now')";
                    break;
            }

            return baseQuery + ";";
        }
    }
}
