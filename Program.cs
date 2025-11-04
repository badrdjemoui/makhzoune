using System;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;


namespace AnimalFeedApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 🔹 تهيئة إعدادات النوافذ
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 🔹 إنشاء قاعدة البيانات والجداول إذا لم تكن موجودة
            DatabaseHelper.InitializeDatabase();

            // 🔹 تشغيل الواجهة الرئيسية
            Application.Run(new FormMain());
        }
    }
}
