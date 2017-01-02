namespace WinUX.Common
{
    using System;

    /// <summary>
    /// Defines a model for providing a weak event listener.
    /// </summary>
    /// <typeparam name="TInstance">
    /// The instance type for the listener.
    /// </typeparam>
    /// <typeparam name="TSource">
    /// The source type.
    /// </typeparam>
    public sealed class WeakEventListener<TInstance, TSource>
        where TInstance : class
    {
        private readonly WeakReference weakReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakEventListener{TInstance,TSource}"/> class.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public WeakEventListener(TInstance instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            this.weakReference = new WeakReference(instance);
        }

        /// <summary>
        /// Gets or sets the action to be fired when the event is triggered.
        /// </summary>
        public Action<TInstance, TSource> OnEventAction { get; set; }

        /// <summary>
        /// Gets or sets the action to be fired when the listener is detached.
        /// </summary>
        public Action<TInstance, WeakEventListener<TInstance, TSource>> OnDetachAction { get; set; }

        /// <summary>
        /// Called when the event is fired.
        /// </summary>
        /// <param name="source">
        /// The source of the event.
        /// </param>
        public void OnEvent(TSource source)
        {
            var target = (TInstance)this.weakReference.Target;
            if (target != null)
            {
                this.OnEventAction?.Invoke(target, source);
            }
            else
            {
                this.Detach();
            }
        }

        /// <summary>
        /// Called when detaching the event listener.
        /// </summary>
        public void Detach()
        {
            var target = (TInstance)this.weakReference.Target;
            if (this.OnDetachAction == null)
            {
                return;
            }

            this.OnDetachAction(target, this);
            this.OnDetachAction = null;
        }
    }

    /// <summary>
    /// Defines a model for providing a weak event listener.
    /// </summary>
    /// <typeparam name="TInstance">
    /// The instance type for the listener.
    /// </typeparam>
    /// <typeparam name="TSource">
    /// The source type.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// The event argument type.
    /// </typeparam>
    public sealed class WeakEventListener<TInstance, TSource, TEventArgs>
        where TInstance : class
    {
        private readonly WeakReference weakInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakEventListener{TInstance,TSource}"/> class.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public WeakEventListener(TInstance instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            this.weakInstance = new WeakReference(instance);
        }

        /// <summary>
        /// Gets or sets the action to be fired when the event is triggered.
        /// </summary>
        public Action<TInstance, TSource, TEventArgs> OnEventAction { get; set; }

        /// <summary>
        /// Gets or sets the action to be fired when the listener is detached.
        /// </summary>
        public Action<TInstance, WeakEventListener<TInstance, TSource, TEventArgs>> OnDetachAction { get; set; }

        /// <summary>
        /// Called when the event is fired.
        /// </summary>
        /// <param name="source">
        /// The source of the event.
        /// </param>
        /// <param name="eventArgs">
        /// The event arguments.
        /// </param>
        public void OnEvent(TSource source, TEventArgs eventArgs)
        {
            var target = (TInstance)this.weakInstance.Target;
            if (target != null)
            {
                this.OnEventAction?.Invoke(target, source, eventArgs);
            }
            else
            {
                this.Detach();
            }
        }

        /// <summary>
        /// Called when detaching the event listener.
        /// </summary>
        public void Detach()
        {
            var target = (TInstance)this.weakInstance.Target;
            if (this.OnDetachAction == null)
            {
                return;
            }

            this.OnDetachAction(target, this);
            this.OnDetachAction = null;
        }
    }
}