using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MotherboardTester
{
    public partial class MainWindow : Window
    {

        public Pair<Element, Element> selected = new Pair<Element, Element>();
        public List<Element> elements = new List<Element>();
        public Motherboard MSI;
        public List<Connect> connects = new List<Connect>();
        MultimeterWindow multimeterWindow;
        DiodeWindow diodeWindow;
        private int answerID;
        Dictionary<int, string> desc;
        ChoiceWindow wnd;


        Element createBox(string name, int left, int top, int w, int h, Motherboard m)
        {
            Rectangle box = new Rectangle();
            box.Name = name;
            box.Fill = new SolidColorBrush(Colors.LightBlue);
            box.Width = w;
            box.Height = h;
            box.Opacity = 0.5;
            box.HorizontalAlignment = HorizontalAlignment.Left;
            box.VerticalAlignment = VerticalAlignment.Center;
            box.MouseLeftButtonDown += rectangle_click;
            Canvas.SetLeft(box, left);
            Canvas.SetTop(box, top);
            Motherboard.Children.Add(box);
            Element el = new Element(false, name, box);
            m.elements.Add(el);
            elements.Add(el);
            return el;
        }


        void handleConn(object sender, EventArgs e)
        {
            if (multimeterWindow != null || diodeWindow != null)
            {
                int typeConn = 0;
                if (selected.First != null) typeConn++;
                if (selected.Second != null) typeConn++;
                foreach (Connect conn in connects)
                {
                    if (typeConn > 0)
                    {
                        switch (conn.connType)
                        {
                            case 0:
                                if(diodeWindow != null)
                                {
                                    diodeWindow.DiodeActivate(conn.values);
                                }
                                break;
                            case 2:
                                if (multimeterWindow != null)
                                {
                                    for(int i = 0; i < conn.conn.Length;i++)
                                    {
                                        if ((conn.conn[i].Second == selected.Second && conn.conn[i].First == selected.First) || (conn.conn[i].First == selected.Second && conn.conn[i].Second == selected.First))
                                        {
                                            multimeterWindow.MeasureVoltageAndResist(conn.values[i * 2].ToString(), conn.values[i * 2 + 1].ToString());
                                            return;
                                        }
                                    }
                                    
                                }
                                break;
                        }

                    }
                }
            }

        }

        private float genValue(float min, float max)
        {
            Random random = new Random();
            double range = (double)max - (double)min;
            double sample = random.NextDouble();
            double scaled = (sample * range) + min;
            return (float)scaled;
        }


        public MainWindow(ChoiceWindow wnd,Dictionary<int,string> answers, int indexOfTrueAnswer, string id)
        {
            InitializeComponent();
            this.wnd = wnd;
            wnd.Visibility = Visibility.Hidden;
            desc = answers;
            answerID = indexOfTrueAnswer;
            selector.ItemsSource = answers.Values.OrderBy((n) => n);

            CompositionTarget.Rendering += handleConn;
            ResizeMode = ResizeMode.NoResize;
            MSI = new Motherboard("MSI-H110-pro-vd-p");
            Element gnd = createBox("GND", 220, 36, 10, 10, MSI);
            
            Element v33 = createBox("v3", 612, 373, 10, 10, MSI);
            Element v332 = createBox("v32", 612, 360, 10, 10, MSI);
            Element v333 = createBox("v33", 628, 373, 10, 10, MSI);

            Element v5 = createBox("v5", 612, 335, 10, 10, MSI);
            Element v52 = createBox("v52", 612, 310, 10, 10, MSI);
            Element v53 = createBox("v53", 627, 247, 10, 10, MSI);
            Element v54 = createBox("v54", 627, 260, 10, 10, MSI);
            Element v55 = createBox("v55", 627, 272, 10, 10, MSI);
            Element v5vsb = createBox("v5vsb", 613, 272, 10, 10, MSI);

            Element v12 = createBox("v12", 613, 247, 10, 10, MSI);
            Element v122 = createBox("v122", 613, 260, 10, 10, MSI);

            Element quartz = createBox("quartz", 376, 620, 10, 10, MSI);

            Element usb1dpdm = createBox("usb1dpdm", 245, 658, 10, 10, MSI);
            Element usb2dpdm = createBox("usb2dpdm", 191, 658, 10, 10, MSI);

            Element kts = createBox("kts", 348, 431, 10, 10, MSI);

            Element bios = createBox("bios", 524, 631, 10, 10, MSI);
            
            Element pciex16 = createBox("pciex16", 208, 479, 300, 22, MSI);

            Element pciex1 = createBox("pciex1", 206, 540, 85, 20, MSI);
            Element pciex12 = createBox("pciex12", 206, 600, 85, 20, MSI);


            string[] valuesGNDV3Multimetr = ["-",genValue(0.803f,0.805f).ToString(), "-", genValue(0.803f, 0.805f).ToString(), "-", genValue(0.803f, 0.805f).ToString()];
            string[] valuesGNDV5Multimetr = ["-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(0.84f, 0.85f).ToString()];
            string[] valuesGNDV12Multimetrr = ["-", genValue(10.3f,10.45f).ToString(), "-", genValue(10.3f, 10.45f).ToString()];
            string[] valuesGNDQuartzMultimetr = ["-", genValue(0.2f, 0.3f).ToString()];
            string[] valuesGNDUSBMultimetr = [ genValue(0.000460f, 0.000465f).ToString(), "-", genValue(0.000460f, 0.000465f).ToString(), "-"];
            string[] valuesGNDKTSMultimetr = [ genValue(2.9f, 3f).ToString(), "-"];
            string[] valuesPCIeTester = [ "16", "16", "16", "4", "4", "4", "4", "4", "4" ];
            string[] valuesBiosDiode = [ "1", "1", "0", "0", "1" ];
            switch (id)
            {
                case "gndV3Multimetr":
                    double rnd = new Random().NextDouble();
                    if(rnd >= 0.0  && rnd < 0.3)
                    {
                        valuesGNDV3Multimetr = ["-", "0", "-", genValue(0.803f, 0.805f).ToString(), "-", genValue(0.803f, 0.805f).ToString()];
                    }
                    else if(rnd >= 0.3 && rnd < 0.6)
                    {
                        valuesGNDV3Multimetr = ["-", genValue(0.803f, 0.805f).ToString(), "-", "0", "-", genValue(0.803f, 0.805f).ToString()];
                    }
                    else if(rnd >= 0.6 && rnd <= 1)
                    {
                        valuesGNDV3Multimetr = ["-", genValue(0.803f, 0.805f).ToString(), "-", genValue(0.803f, 0.805f).ToString(), "-", "0"];
                    }
                    break;
                case "gndV5Multimetr":
                    rnd = new Random().NextDouble();
                    if (rnd >= 0.0 && rnd < 0.3)
                    {
                        valuesGNDV5Multimetr = ["-", genValue(2.15f, 2.17f).ToString(), "-", "0", "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(0.84f, 0.85f).ToString()];
                    }
                    else if (rnd >= 0.3 && rnd < 0.6)
                    {
                        valuesGNDV5Multimetr = ["-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", "0", "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(0.84f, 0.85f).ToString()];
                    }
                    else if (rnd >= 0.6 && rnd <= 1)
                    {
                        valuesGNDV5Multimetr = ["-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-", genValue(2.15f, 2.17f).ToString(), "-","0"];
                    }
                    break;
                case "gndV12Multimetr":
                    rnd = new Random().NextDouble();
                    if (rnd >= 0.0 && rnd < 0.5)
                    {
                        valuesGNDV12Multimetrr = ["-", "0", "-", genValue(10.3f, 10.45f).ToString()];
                    }
                    else if (rnd >= 0.5 && rnd <= 1)
                    {
                        valuesGNDV12Multimetrr = ["-", genValue(10.3f, 10.45f).ToString(), "-", "0"];
                    }  
                    break;
                case "gndQuartzMultimetr":
                    valuesGNDQuartzMultimetr = ["-", genValue(0.5f, 0.8f).ToString()];
                    break;
                case "gndUSBMultimetr":
                    valuesGNDUSBMultimetr = [genValue(0.000933f, 0.001024f).ToString(), "-", genValue(0.000650f, 0.000700f).ToString(), "-"];
                    break;
                case "gndKTSMultimetr":
                    valuesGNDKTSMultimetr = [genValue(2.2f, 2.7f).ToString(), "-"];
                    break;
                case "pcieTester":
                    Random r = new Random();
                    valuesPCIeTester = [r.Next(1,16).ToString(), r.Next(1, 16).ToString(), "16", r.Next(1, 4).ToString(), r.Next(1, 4).ToString(), "4", r.Next(1, 4).ToString(), r.Next(1, 4).ToString(), "4"];
                    break;
                case "biosDiode":
                    valuesBiosDiode = ["0", "0", "0", "0", "0"];
                    break;
            }

            Connect gndV3Multimetr = new Connect(valuesGNDV3Multimetr, 2, [new Pair<Element, Element>(gnd, v33), new Pair<Element, Element>(gnd, v332), new Pair<Element, Element>(gnd, v333)]);
            Connect gndV5Multimetr = new Connect(valuesGNDV5Multimetr, 2, [new Pair<Element, Element>(gnd, v5), new Pair<Element, Element>(gnd, v52), new Pair<Element, Element>(gnd, v53), new Pair<Element, Element>(gnd, v54), new Pair<Element, Element>(gnd, v55), new Pair<Element, Element>(gnd, v5vsb)]);
            Connect gndV12Multimetr = new Connect(valuesGNDV12Multimetrr, 2, [new Pair<Element, Element>(gnd, v12), new Pair<Element, Element>(gnd, v122)]);
            Connect gndQuartzMultimetr = new Connect(valuesGNDQuartzMultimetr, 2, [new Pair<Element, Element>(gnd, quartz)]);
            Connect gndUSBMultimetr = new Connect(valuesGNDUSBMultimetr, 2, [new Pair<Element, Element>(gnd, usb1dpdm), new Pair<Element, Element>(gnd, usb2dpdm)]);
            Connect gndKTSMultimetr = new Connect(valuesGNDKTSMultimetr, 2 , [new Pair<Element, Element>(gnd, kts)]); // V, kOm

            Connect pcieTester = new Connect(valuesPCIeTester, 1, [new Pair<Element, Element>(pciex16, pciex16), new Pair<Element, Element>(pciex1, pciex1), new Pair<Element, Element>(pciex12, pciex12)]);

            Connect biosDiode = new Connect(valuesBiosDiode, 0, []);

            connects.Add(gndV3Multimetr);
            connects.Add(gndV5Multimetr);
            connects.Add(gndV12Multimetr);
            connects.Add(gndQuartzMultimetr);
            connects.Add(gndUSBMultimetr);
            connects.Add(gndKTSMultimetr);
            connects.Add(pcieTester);
            connects.Add(biosDiode);

            //MSI.elements.Add(new Element(false,"gnd"));
            //test.Children.Clear();
        }

        void rectangle_click(object sender, MouseButtonEventArgs e)
        {
            Rectangle rec = (Rectangle)e.Source;
            if ((selected.First != null && selected.First.checkBox == rec ) || (selected.Second != null && selected.Second.checkBox == rec)) 
            {
                
                if(selected.First != null && selected.First.checkBox == rec)
                {
                    rec.Fill = new SolidColorBrush(Colors.LightBlue);
                    selected.First = null;
                    if (multimeterWindow != null)
                        multimeterWindow.MeasureVoltageAndResist("-", "-");
                }
                else
                {
                    rec.Fill = new SolidColorBrush(Colors.LightBlue);
                    selected.Second = null;
                    if(multimeterWindow != null)
                        multimeterWindow.MeasureVoltageAndResist("-", "-");

                }
            }
            else
            {
                Element el = elements.Find((Element el) => el.id == rec.Name);
                if (el!= null && selected.First == null && selected.Second != el)
                {
                    selected.First = el;
                    rec.Fill = new SolidColorBrush(Colors.LightPink);
                }
                else if(el != null &&  selected.Second == null && selected.First != el) 
                {
                    selected.Second = el;
                    rec.Fill = new SolidColorBrush(Colors.DarkBlue);
                }
            }
        }

        private void PCIETestetButton_Click(object sender, EventArgs e) 
        {
            foreach (Connect conn in connects)
            {
                if (conn.connType == 1)
                {
                    for (int i = 0; i < conn.conn.Length; i++)
                    {
                        if ((selected.First == null || selected.Second == null) && (conn.conn[i].Second == selected.Second || conn.conn[i].First == selected.First))
                        {
                            float TX = int.Parse(conn.values[i * 3]);
                            float RX = int.Parse(conn.values[i * 3 + 1]);
                            float LINES  = int.Parse(conn.values[i * 3 + 2]);
                            if(TX == RX &&  LINES == TX)
                            {
                                string answer = "";
                                for(int j = 0;j<LINES;j++)
                                {
                                    answer += "RXP" + j + ": 1 " + "TXP" + j + ": 1" + '\n';
                                    answer += "RXN" + j + ": 1 " + "TXN" + j + ": 1" + '\n';
                                }
                                MessageBox.Show(answer);
                            }
                            else if (LINES == RX) 
                            {
                                string answer = "";
                                for (int j = 0; j < LINES; j++)
                                {
                                    answer += "RXP" + j + ": 1 " + "TXP" + j +  ((j<TX)? ": 1" : ": 0") + '\n';
                                    answer += "RXN" + j + ": 1 " + "TXN" + j + ((j < TX) ? ": 1" : ": 0") + '\n';
                                }
                                MessageBox.Show(answer);
                            }
                            else if (LINES == TX) 
                            {
                                string answer = "";
                                for (int j = 0; j < LINES; j++)
                                {
                                    answer += "RXP" + j + ((j < TX) ? ": 1 " : ": 0 ") + "TXP" + j + ": 1" + '\n';
                                    answer += "RXN" + j + ((j < TX) ? ": 1 " : ": 0 ") + "TXN" + j + ": 1" + '\n';
                                }
                                MessageBox.Show(answer);
                            }
                            else
                            {
                                string answer = "";
                                for (int j = 0; j < LINES; j++)
                                {
                                    answer += "RXP" + j + ((j < TX) ? ": 1 " : ": 0 ") + "TXP" + j + ((j < TX) ? ": 1" : ": 0") + '\n';
                                    answer += "RXN" + j + ((j < TX) ? ": 1 " : ": 0 ") + "TXN" + j + ((j < TX) ? ": 1" : ": 0") + '\n';
                                }
                                MessageBox.Show(answer);
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void MultimeterButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно мультиметра
            multimeterWindow = new MultimeterWindow();
            multimeterWindow.Owner = this;
            multimeterWindow.Show();
        }

        private void OscilloscopeButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно осциллографа
            OscilloscopeWindow oscilloscopeWindow = new()
            {
                Owner = this
            };
            oscilloscopeWindow.ShowDialog();
        }

        private void LayoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно осциллографа
            LayoutWindow layoutWindow = new()
            {
                Owner = this
            };
            layoutWindow.Show();
        }

        private void DiodeButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно осциллографа
            diodeWindow = new()
            {
                Owner = this
            };
            diodeWindow.Show();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(selector.SelectedItem.ToString() == desc[answerID])
            {
                MessageBox.Show("Ответ выбран правильно");
                wnd.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Ответ выбран неправильно");
                ChoiceWindow wnd = new ChoiceWindow();
                wnd.Show();
            }
            Close();
        }
    }

}
