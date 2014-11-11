using System;

namespace XamarinLastfm
{

	public class MusicFactory
	{
		public static IMusicRepository GetMusicData(MusicData type)
		{

			switch (type)
			{
				case MusicData.TestData:

					return new TestData();

				case MusicData.LFMWebService:

					return new LFMWebService ();

				default:

					return null;
			}

		}
	}
}

