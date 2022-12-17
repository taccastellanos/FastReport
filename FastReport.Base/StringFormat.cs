    public class StringFormat 
    {
        public StringFormat()
        {
            Alignment = StringAlignment.Center;
            FormatFlags =  StringFormatFlags.NoWrap;
            DigitSubstitutionMethod = StringDigitSubstitute.User;
            Trimming = StringTrimming.Word;
            HotkeyPrefix = HotkeyPrefix.Show;
            LineAlignment = StringAlignment.Center;
            
        }
        //
        // Summary:
        //     Gets a generic typographic System.Drawing.StringFormat object.
        //
        // Returns:
        //     A generic typographic System.Drawing.StringFormat object.
        public static StringFormat GenericTypographic { get; }
        //
        // Summary:
        //     Gets a generic default System.Drawing.StringFormat object.
        //
        // Returns:
        //     The generic default System.Drawing.StringFormat object.
        public static StringFormat GenericDefault { get; }
        //
        // Summary:
        //     Gets or sets the System.Drawing.Text.HotkeyPrefix object for this System.Drawing.StringFormat
        //     object.
        //
        // Returns:
        //     The System.Drawing.Text.HotkeyPrefix object for this System.Drawing.StringFormat
        //     object, the default is System.Drawing.Text.HotkeyPrefix.None.
        //public HotkeyPrefix HotkeyPrefix { get; set; }
        //
        // Summary:
        //     Gets or sets a System.Drawing.StringFormatFlags enumeration that contains formatting
        //     information.
        //
        // Returns:
        //     A System.Drawing.StringFormatFlags enumeration that contains formatting information.
        public StringFormatFlags FormatFlags { get; set; }
        //
        // Summary:
        //     Gets the method to be used for digit substitution.
        //
        // Returns:
        //     A System.Drawing.StringDigitSubstitute enumeration value that specifies how to
        //     substitute characters in a string that cannot be displayed because they are not
        //     supported by the current font.
        public StringDigitSubstitute DigitSubstitutionMethod { get; }
        //
        // Summary:
        //     Gets the language that is used when local digits are substituted for western
        //     digits.
        //
        // Returns:
        //     A National Language Support (NLS) language identifier that identifies the language
        //     that will be used when local digits are substituted for western digits. You can
        //     pass the System.Globalization.CultureInfo.LCID property of a System.Globalization.CultureInfo
        //     object as the NLS language identifier. For example, suppose you create a System.Globalization.CultureInfo
        //     object by passing the string "ar-EG" to a System.Globalization.CultureInfo constructor.
        //     If you pass the System.Globalization.CultureInfo.LCID property of that System.Globalization.CultureInfo
        //     object along with System.Drawing.StringDigitSubstitute.Traditional to the System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)
        //     method, then Arabic-Indic digits will be substituted for western digits at display
        //     time.
        public int DigitSubstitutionLanguage { get; }
        //
        // Summary:
        //     Gets or sets horizontal alignment of the string.
        //
        // Returns:
        //     A System.Drawing.StringAlignment enumeration that specifies the horizontal alignment
        //     of the string.
        public StringAlignment Alignment { get; set; }
        //
        // Summary:
        //     Gets or sets the System.Drawing.StringTrimming enumeration for this System.Drawing.StringFormat
        //     object.
        //
        // Returns:
        //     A System.Drawing.StringTrimming enumeration that indicates how text drawn with
        //     this System.Drawing.StringFormat object is trimmed when it exceeds the edges
        //     of the layout rectangle.
        public StringTrimming Trimming { get; set; }
        //
        // Summary:
        //     Gets or sets the vertical alignment of the string.
        //
        // Returns:
        //     A System.Drawing.StringAlignment enumeration that represents the vertical line
        //     alignment.
        public StringAlignment LineAlignment { get; set; }

        public HotkeyPrefix HotkeyPrefix { get; set; }

        public float[] GetTabStops(out float first)
        {
            var f = new float[]{1,2,3};
            first = f[0];
            return f;
        }
        public void SetTabStops(float firstTabOffset, float[] tabStops)
        {
            /*TODO:*/
        }
    }