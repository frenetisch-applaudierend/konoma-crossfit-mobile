using System.Threading.Tasks;
using Android.Content;

namespace Konoma.CrossFit
{
    public class PushActivityNavigation<TScene> : INavigation<TScene>
        where TScene : Scene
    {
        private readonly Context _context;
        private readonly Intent _intent;

        public PushActivityNavigation(Context context, Intent intent)
        {
            _context = context;
            _intent = intent;
        }

        public Task NavigateAsync(bool animated)
        {
            _context.StartActivity(_intent);
            return Task.CompletedTask;
        }
    }

    public static partial class AndroidNavigation
    {
        public static INavigation<TScene> PushActivity<TScene, TActivity>(Context context)
            where TScene : Scene
            where TActivity : CrossFitActivity<TScene>
        {
            var intent = new Intent(context, typeof(TActivity));
            return new PushActivityNavigation<TScene>(context, intent);
        }
     }
}
