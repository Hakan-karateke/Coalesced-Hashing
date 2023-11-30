using CoalescedHashing;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using ScottPlot.WinForms;
using static CoalescedHashing.ColaescedHashingFunctions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ScottPlot;

namespace CoalescedHashing
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        public const int VeriMiktar = 100000;
        int[]? ints;
        ColaescedHashingFunctions.LICH LICH = new ColaescedHashingFunctions.LICH(VeriMiktar);
        ColaescedHashingFunctions.EICH EICH=new ColaescedHashingFunctions.EICH(VeriMiktar);
        ColaescedHashingFunctions.LISCH LISCH = new ColaescedHashingFunctions.LISCH(VeriMiktar);
        ColaescedHashingFunctions.BLISCH BLISCH = new ColaescedHashingFunctions.BLISCH(VeriMiktar);
        ColaescedHashingFunctions.EISCH EISCH = new ColaescedHashingFunctions.EISCH(VeriMiktar);
        ColaescedHashingFunctions.REISCH REISCH = new ColaescedHashingFunctions.REISCH(VeriMiktar);
        ColaescedHashingFunctions.RLISCH RLISCH = new ColaescedHashingFunctions.RLISCH(VeriMiktar);
        ColaescedHashingFunctions.BEISCH BEISCH = new ColaescedHashingFunctions.BEISCH(VeriMiktar);

        // LiveCharts yerine ScottPlot kullanýlýyor.
        private readonly FormsPlot formsPlotInsertTimes = new FormsPlot();
        private readonly FormsPlot formsPlotCollisionAmounts = new FormsPlot();

        public Form1()
        {
            InitializeComponent();
            // Form yüklenirken FormsPlot'larý ekleyin
            Controls.Add(formsPlotInsertTimes);
            Controls.Add(formsPlotCollisionAmounts);

            // formsPlotInsertTimes
            this.formsPlotInsertTimes.Location = new System.Drawing.Point(100, 100); // X ve Y konumunu ayarlayýn
            this.formsPlotInsertTimes.Size = new System.Drawing.Size(400, 300); // Geniþlik ve yüksekliði ayarlayýn
            this.formsPlotInsertTimes.TabIndex = 0;

            // formsPlotCollisionAmounts
            this.formsPlotCollisionAmounts.Location = new System.Drawing.Point(550, 100); // X ve Y konumunu ayarlayýn
            this.formsPlotCollisionAmounts.Size = new System.Drawing.Size(400, 300); // Geniþlik ve yüksekliði ayarlayýn
            this.formsPlotCollisionAmounts.TabIndex = 1;
        }

        private void btnPerformans_Click(object sender, EventArgs e)
        {   
            LICH.CakismaMiktarý = 0; 
            LISCH.CakismaMiktarý = 0;
            BLISCH.CakismaMiktarý = 0;
            EISCH.CakismaMiktarý = 0;
            REISCH.CakismaMiktarý = 0;
            BEISCH.CakismaMiktarý = 0;
            RLISCH.CakismaMiktarý = 0;
            ColaescedHashingFunctions.Results.Clear();

            ints = new int[Convert.ToInt32(comboBoxMiktar.Text) * 1000];
            // Örnek veri seti oluþtur
            Random random = new Random();
            for (int i = 0; i < Convert.ToInt32(comboBoxMiktar.Text) * 100; i++)
            {
                ints[i] = (random.Next(1000, (1000 + VeriMiktar))); // veri setinizi nasýl oluþturduðum
            }
            ColaescedHashingFunctions.CalculateResults(ints, LISCH, LICH, EISCH, REISCH, RLISCH, BLISCH, BEISCH, EICH);

            // ScottPlot için kullanýlacak veri yapýlarýný oluþturun
            double[] insertTimes = new double[ColaescedHashingFunctions.Results.Count];
            double[] collisionAmounts = new double[ColaescedHashingFunctions.Results.Count];
            string[] algorithmNames = new string[8]
            {
                "LISCH", "LICH", "EICH", "EISCH", "REISCH", "RLISCH", "BLISCH", "BEISCH"
            };

            // Verileri doldurun
            for (int i = 0; i < ColaescedHashingFunctions.Results.Count; i++)
            {
                insertTimes[i] = ColaescedHashingFunctions.Results[i].InsertTime;
                collisionAmounts[i] = ColaescedHashingFunctions.Results[i].Cakisma;
            }

            // Grafikleri oluþturun
            CreateGraph(insertTimes, "Ekleme Süresi (milisaniye)", formsPlotInsertTimes, algorithmNames);
            CreateGraph(collisionAmounts, "Çakýþma Miktarý", formsPlotCollisionAmounts, algorithmNames);
        }

        // ScottPlot ile grafik oluþturmak için yardýmcý metod
        private void CreateGraph(double[] values, string title, FormsPlot formsPlot, string[] algorithmNames)
        {
            formsPlot.Plot.Clear();
            formsPlot.Plot.Title(title);
            formsPlot.Plot.YLabel(title);
            formsPlot.Plot.XTicks(algorithmNames);
            // Seriyi ekle
            formsPlot.Plot.AddBar(values);


            // Eksen etiketlerini güncelle
            formsPlot.Render();

            // Eksen etiketlerini güncelle
            formsPlot.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bu program, Coalesced Hashing algoritmalarýný seçtiðiniz veri miktarý kullanarak karþýlaþtýrýr.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboBoxMiktar.SelectedIndex = 0;
            toolTip1.SetToolTip(btnPerformans, "Bu butona týkladýðýnýzda sonuçlarýn karþýlaþtýrýlmasýný göreceksiniz.");
            toolTip1.SetToolTip(comboBoxMiktar, "burada seçeceðiniz yüzdelik dilim fonksiyonun listede dolduracaðý veri miktarý ile ilgilidir");
        }
    }
}
