namespace ly
{
    static class Log
    {
        public static void V(string message)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[verbose]{0}", message));
        }

        public static void E(string message)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[error]{0}", message));
        }

        public static void W(string message)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[warn]{0}", message));
        }

        public static void I(string message)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[info]{0}", message));
        }

        public static void D(string message)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("[debug]{0}", message));
        }
    }
}
