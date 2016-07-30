namespace Xamarin.Forms
{
    /// <summary>
    /// TableIntent provides hints to the renderer about how a table will be used.
    /// </summary>
    /// <remarks>
    /// https://developer.xamarin.com/api/type/Xamarin.Forms.TableIntent/
    /// Using table intents will not effect the behavior of a table, 
    /// ʹ����������������Ϊ�ϵ�Ч����
    /// and will only modify their visual appearance on screen, depending on the platform. 
    /// ֻ���޸���Ļ�Ͽ��ӻ�����ۣ������������ھ���ƽ̨��
    /// Not all intents are unique on all platforms, 
    /// however it is advisable to pick the intent which most closely represents your use case.
    /// </remarks>
	public enum TableIntent
	{
        /// <summary>
        /// A table intended to be used as a menu for selections.
        /// ѡ�е�ʱ����Ϊ�˵�һ��ʹ��
        /// </summary>
		Menu,
        /// <summary>
        /// A table containing a set of switches, toggles, or other modifiable configuration settings.
        /// ���ý���
        /// </summary>
		Settings,
        /// <summary>
        /// A table which is used to contain information that would normally be contained in a form.
        /// һ����Ϣ�ύ��
        /// </summary>
		Form,
        /// <summary>
        /// A table intended to contain an arbitrary number of similar data entries.
        /// ����������������ͬ������Ԫ��
        /// </summary>
		Data
    }
}