namespace Xamarin.Forms
{
    /// <summary>
    /// TableIntent provides hints to the renderer about how a table will be used.
    /// </summary>
    /// <remarks>
    /// https://developer.xamarin.com/api/type/Xamarin.Forms.TableIntent/
    /// Using table intents will not effect the behavior of a table, 
    /// 使用它，将不会有行为上的效果，
    /// and will only modify their visual appearance on screen, depending on the platform. 
    /// 只会修改屏幕上可视化的外观，并且它依赖于具体平台，
    /// Not all intents are unique on all platforms, 
    /// however it is advisable to pick the intent which most closely represents your use case.
    /// </remarks>
	public enum TableIntent
	{
        /// <summary>
        /// A table intended to be used as a menu for selections.
        /// 选中的时候被作为菜单一样使用
        /// </summary>
		Menu,
        /// <summary>
        /// A table containing a set of switches, toggles, or other modifiable configuration settings.
        /// 设置界面
        /// </summary>
		Settings,
        /// <summary>
        /// A table which is used to contain information that would normally be contained in a form.
        /// 一个信息提交表单
        /// </summary>
		Form,
        /// <summary>
        /// A table intended to contain an arbitrary number of similar data entries.
        /// 包含任意数量的相同的数据元素
        /// </summary>
		Data
    }
}