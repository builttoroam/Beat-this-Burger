using BeatThisBurger.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatThisBurger.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewBurgerView 
    {
        public NewBurgerView()
        {
            InitializeComponent();
            Data = new DataContextWrapper<NewBurgerViewModel>(this);
        }

        public DataContextWrapper<NewBurgerViewModel> Data { get; private set; }



    }
}
