using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AnswerAppBrayanRojas.Models;

namespace AnswerAppBrayanRojas.ViewModels
{
    public class AskViewModel : BaseViewModel
    {
        public Ask MyQuetion { get; set; }
        
        public AskViewModel()
        {
            MyQuetion = new Ask();
        }

        public async Task<ObservableCollection<Ask>> GetQlist()
        {
            if (IsBusy)
            {
                return null;
            }
            else
            {
                IsBusy = true;

                try
                {

                    ObservableCollection<Ask> list = new ObservableCollection<Ask>();

                    list = await MyQuetion.GetQuestionsListByUser();

                    if (list == null)
                    {
                        return null;
                    }
                    else
                    {
                        return list;
                    }

                }
                catch (Exception)
                {
                    return null;
                }
                finally { IsBusy = false; }
            }
        }
    }
}
