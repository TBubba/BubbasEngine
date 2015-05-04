
namespace BubbasEngine.Engine.Graphics.Rendering
{
    public class GraphicsLayer
    {
        // Private
        private RenderableContainer _renderables;
        private View _view;
        private ViewRef _defView;

        private bool _hide;

        // Public
        public RenderableContainer Renderables
        { get { return _renderables; } }
        public View View
        { get { return _view; } }

        public bool Hide
        { get { return _hide; } set { _hide = value; } }

        // Constructor(s)
        internal GraphicsLayer(ViewRef defView)
        {
            _renderables = new RenderableContainer();
            _defView = defView;
        }

        // View
        public void SetView(View view)
        {
            _view = view;
        }
        public View GetView()
        {
            if (_view != null)
                return _view;
            return _defView.View;
        }

        // Render
        internal void Render(IRenderTarget target)
        {
            // Abort if hidden
            if (_hide)
                return;

            // Set view
            if (_view != null)
                target.SetView(_view);
            else
                target.SetView(_defView.View);

            // Render
            _renderables.Render(target);
        }
        internal void RenderTo(IRenderTarget target)
        {
            _renderables.Render(target);
        }
    }
}
