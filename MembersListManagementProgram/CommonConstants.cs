namespace MembersListManagementProgram
{
	public static class CommonConstants
	{
		/// <summary>
		/// 指定するマスタ画面区分
		/// </summary>
		public static class MasterMode
		{
			// 部門
			public static readonly string BUMON = "1";
			// 社員
			public static readonly string SYAIN = "2";
		}

		/// <summary>
		/// 編集区分
		/// </summary>
		public static class EditMode
		{
			// 新規作成
			public static readonly string CREATE_MODE = "1";
			// 更新
			public static readonly string UPDATE_MODE = "2";
			// 参照
			public static readonly string VIEW_MODE = "3";
		}
	}
}
