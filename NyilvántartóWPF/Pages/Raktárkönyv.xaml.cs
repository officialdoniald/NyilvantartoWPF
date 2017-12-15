using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace NyilvántartóWPF.Pages
{
    public partial class Raktárkönyv : Page
    {
        #region attribútumok

        string[] szuro_combobox_tomb = new string[4] { "", "anyag", "főcsoport", "gyártó" };

        string selected_szuro_combobox = "";

        string ésmitt = "";

        ObservableCollection<mik> mik_anyagok = new ObservableCollection<mik>();
        ObservableCollection<mik> mik_főcsoport = new ObservableCollection<mik>();
        ObservableCollection<mik> mik_gyártó = new ObservableCollection<mik>();

        NyilvantartoDAODBImpl db_implementation = new NyilvantartoDAODBImpl();

        #endregion

        #region init

        public Raktárkönyv()
        {
            InitializeComponent();
            foreach (var item in db_implementation.getTörzsadlista().OrderBy(a=>a.anyagnév))
            {
                mik mi = new mik();
                mi.mi = item.anyagnév;
                mik_anyagok.Add(mi);
            }
            string volte = "";
            foreach (var item in db_implementation.getTörzsadlista().OrderBy(a => a.főcsoport))
            {
                if (volte != item.főcsoport)
                {
                    volte = item.főcsoport;
                    mik mi = new mik();
                    mi.mi = volte;
                    mik_főcsoport.Add(mi);
                }
            }
            volte = "";
            foreach (var item in db_implementation.getTörzsadlista().OrderBy(a => a.gyártó))
            {
                if (volte != item.gyártó)
                {
                    volte = item.gyártó;
                    mik mi = new mik();
                    mi.mi = volte;
                    mik_gyártó.Add(mi);
                }
            }
        }

        #endregion

        #region selectionChanged

        private void szuro_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = 0;
            selectedIndex = szuro_combobox.SelectedIndex;
            selected_szuro_combobox = szuro_combobox_tomb[selectedIndex];

            ésmit.ItemsSource = null;
            if (selected_szuro_combobox == "anyag")
            {
                ésmit.ItemsSource = mik_anyagok;
            }
            else if (selected_szuro_combobox == "főcsoport")
            {
                ésmit.ItemsSource = mik_főcsoport;
            }
            else if (selected_szuro_combobox == "gyártó")
            {
                ésmit.ItemsSource = mik_gyártó;
            }
        }

        private void ésmit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;
            if (selected_szuro_combobox == "anyag")
            {
                foreach (var item in mik_anyagok)
                {
                    if (index == ésmit.SelectedIndex)
                    {
                        ésmitt = item.mi;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else if(selected_szuro_combobox == "főcsoport")
            {
                foreach (var item in mik_főcsoport)
                {
                    if (index == ésmit.SelectedIndex)
                    {
                        ésmitt = item.mi;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else if(selected_szuro_combobox == "gyártó")
            {
                foreach (var item in mik_gyártó)
                {
                    if (index == ésmit.SelectedIndex)
                    {
                        ésmitt = item.mi;
                        break;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }

        #endregion

        #region clicks

        private void mehet_a_raktarkonyv_pdf_button_Click(object sender, RoutedEventArgs e)
        {
            new PDF_Raktarkonyv(selected_szuro_combobox, ésmitt);
        }
        
        #endregion
    }
    public class mik
    {
        public string mi { get; set; }
    }
}