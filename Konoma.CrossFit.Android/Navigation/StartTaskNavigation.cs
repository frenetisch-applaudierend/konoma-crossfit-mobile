using System.Threading.Tasks;
using Android.App;
using Android.Content;

namespace Konoma.CrossFit
{
    public class StartTaskNavigation<TScene, TActivity> : INavigation<TScene>
        where TScene : Scene
        where TActivity : CrossFitActivity<TScene>
    {
        public StartTaskNavigation(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public Task NavigateAsync(bool animated)
        {
            var intent = new Intent(_context, typeof(TActivity));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            _context.StartActivity(intent);

            return Task.CompletedTask;
        }
    }

    public static partial class AndroidNavigation
    {
        public static INavigation<TScene> StartTask<TScene, TActivity>(Context context)
            where TScene : Scene
            where TActivity : CrossFitActivity<TScene>
        {
            return new StartTaskNavigation<TScene, TActivity>(context);
        }
    }
}
