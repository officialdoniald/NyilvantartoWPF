using NyilvántartóWPF.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NyilvántartóWPF
{
     public partial class MainWindow : Window
     {

          #region init
          public MainWindow()
          {
               InitializeComponent();
          }

          #endregion
          
          #region frame_navigation

          private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               if (rendeles_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Ügyfél_rendelés.xaml", UriKind.Relative));                 
               }
               else if (meglevok_listazasa_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Rendelés_véglegesítése());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Rendelés_véglegesítése.xaml", UriKind.Relative));
               }
               else if (ugyfel_felvetel_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_felvétel());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Ügyfél_felvétel.xaml", UriKind.Relative));
               }
               else if (ugygelek_szerkesztése_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Ügyfél_Listázása.xaml", UriKind.Relative));
               }
               else if (törzsadatfelvete_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Törzsadat_felvétel.xaml", UriKind.Relative));
               }
               else if (törzsadatliste_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Törzsadat_listázása.xaml", UriKind.Relative));
               }
               else if (cégrendelés_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Üres_rendelés.xaml", UriKind.Relative));
               }
               else if (beszallito_felvetel_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Beszállító_felvétele.xaml", UriKind.Relative));
               }
               else if (beszallito_listazasa_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Beszállító_liszázasa.xaml", UriKind.Relative));
               }
               else if(raktárkönyv_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Raktárkönyv.xaml", UriKind.Relative));
               }
               else if(cégrendelés_szerkesztése_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Ügyfél_rendelés());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Üres_rendelés_szerkesztése.xaml", UriKind.Relative));
               }
               else if (behozta_a_beszallito_listviewitem.IsSelected == true)
               {
                    fooldal_grid.Visibility = Visibility.Collapsed;
                    _NavigationFrame.Navigate(new Behozta_a_beszallito());
                    _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Behozta_a_beszallito.xaml", UriKind.Relative));
            }
            else if (lista_listviewitem.IsSelected == true)
            {
                fooldal_grid.Visibility = Visibility.Collapsed;
                _NavigationFrame.Navigate(new Behozta_a_beszallito());
                _NavigationFrame.NavigationService.Navigate(new Uri("Pages/Listák.xaml", UriKind.Relative));
            }
            else if (meglevok_listazasamod_listviewitem.IsSelected == true)
            {
                fooldal_grid.Visibility = Visibility.Collapsed;
                _NavigationFrame.Navigate(new UgyfelRendelesModositas());
                _NavigationFrame.NavigationService.Navigate(new Uri("Pages/UgyfelRendelesModositas.xaml", UriKind.Relative));
            }
            


          }

          #endregion
          
     }
}
