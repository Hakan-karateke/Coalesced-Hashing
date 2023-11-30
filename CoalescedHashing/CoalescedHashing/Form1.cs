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

        // LiveCharts yerine ScottPlot kullan�l�yor.
        private readonly FormsPlot formsPlotInsertTimes = new FormsPlot();
        private readonly FormsPlot formsPlotCollisionAmounts = new FormsPlot();

        public Form1()
        {
            InitializeComponent();
            // Form y�klenirken FormsPlot'lar� ekleyin
            Controls.Add(formsPlotInsertTimes);
            Controls.Add(formsPlotCollisionAmounts);

            // formsPlotInsertTimes
            this.formsPlotInsertTimes.Location = new System.Drawing.Point(100, 100); // X ve Y konumunu ayarlay�n
            this.formsPlotInsertTimes.Size = new System.Drawing.Size(400, 300); // Geni�lik ve y�ksekli�i ayarlay�n
            this.formsPlotInsertTimes.TabIndex = 0;

            // formsPlotCollisionAmounts
            this.formsPlotCollisionAmounts.Location = new System.Drawing.Point(550, 100); // X ve Y konumunu ayarlay�n
            this.formsPlotCollisionAmounts.Size = new System.Drawing.Size(400, 300); // Geni�lik ve y�ksekli�i ayarlay�n
            this.formsPlotCollisionAmounts.TabIndex = 1;
        }

        private void btnPerformans_Click(object sender, EventArgs e)
        {   
            LICH.CakismaMiktar� = 0; 
            LISCH.CakismaMiktar� = 0;
            BLISCH.CakismaMiktar� = 0;
            EISCH.CakismaMiktar� = 0;
            REISCH.CakismaMiktar� = 0;
            BEISCH.CakismaMiktar� = 0;
            RLISCH.CakismaMiktar� = 0;
            ColaescedHashingFunctions.Results.Clear();

            ints = new int[Convert.ToInt32(comboBoxMiktar.Text) * 1000];
            // �rnek veri seti olu�tur
            Random random = new Random();
            for (int i = 0; i < Convert.ToInt32(comboBoxMiktar.Text) * 100; i++)
            {
                ints[i] = (random.Next(1000, (1000 + VeriMiktar))); // veri setinizi nas�l olu�turdu�um
            }
            ColaescedHashingFunctions.CalculateResults(ints, LISCH, LICH, EISCH, REISCH, RLISCH, BLISCH, BEISCH, EICH);

            // ScottPlot i�in kullan�lacak veri yap�lar�n� olu�turun
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

            // Grafikleri olu�turun
            CreateGraph(insertTimes, "Ekleme S�resi (milisaniye)", formsPlotInsertTimes, algorithmNames);
            CreateGraph(collisionAmounts, "�ak��ma Miktar�", formsPlotCollisionAmounts, algorithmNames);
        }

        // ScottPlot ile grafik olu�turmak i�in yard�mc� metod
        private void CreateGraph(double[] values, string title, FormsPlot formsPlot, string[] algorithmNames)
        {
            formsPlot.Plot.Clear();
            formsPlot.Plot.Title(title);
            formsPlot.Plot.YLabel(title);
            formsPlot.Plot.XTicks(algorithmNames);
            // Seriyi ekle
            formsPlot.Plot.AddBar(values);


            // Eksen etiketlerini g�ncelle
            formsPlot.Render();

            // Eksen etiketlerini g�ncelle
            formsPlot.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bu program, Coalesced Hashing algoritmalar�n� se�ti�iniz veri miktar� kullanarak kar��la�t�r�r.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboBoxMiktar.SelectedIndex = 0;
            toolTip1.SetToolTip(btnPerformans, "Bu butona t�klad���n�zda sonu�lar�n kar��la�t�r�lmas�n� g�receksiniz.");
            toolTip1.SetToolTip(comboBoxMiktar, "burada se�ece�iniz y�zdelik dilim fonksiyonun listede dolduraca�� veri miktar� ile ilgilidir");
        }
    }
}
