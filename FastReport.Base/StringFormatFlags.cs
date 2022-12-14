public enum StringFormatFlags
    {
        //
        // Summary:
        //     Specifies that text is right to left.
        DirectionRightToLeft = 1,
        //
        // Summary:
        //     Specifies that text is vertical.
        DirectionVertical = 2,
        //
        // Summary:
        //     Specifies that no part of any glyph overhangs the bounding rectangle. By default
        //     some glyphs overhang the rectangle slightly where necessary to appear at the
        //     edge visually. For example when an italic lower case letter f in a font such
        //     as Garamond is aligned at the far left of a rectangle, the lower part of the
        //     f will reach slightly further left than the left edge of the rectangle. Setting
        //     this flag will ensure no painting outside the rectangle but will cause the aligned
        //     edges of adjacent lines of text to appear uneven. WARNING: The GDI+ equivalent
        //     for this is StringFormatFlags::StringFormatFlagsNoFitBlackBox, which is defined
        //     as 0x4. This was a mistake introduced since the first version of the product
        //     and fixing it at this point would be a breaking change. https://docs.microsoft.com/en-us/windows/desktop/api/gdiplusenums/ne-gdiplusenums-stringformatflags
        FitBlackBox = 4,
        //
        // Summary:
        //     Causes control characters such as the left-to-right mark to be shown in the output
        //     with a representative glyph.
        DisplayFormatControl = 32,
        //
        // Summary:
        //     Disables fallback to alternate fonts for characters not supported in the requested
        //     font. Any missing characters are displayed with the fonts missing glyph, usually
        //     an open square.
        NoFontFallback = 1024,
        //
        // Summary:
        //     Specifies that the space at the end of each line is included in a string measurement.
        MeasureTrailingSpaces = 2048,
        //
        // Summary:
        //     Specifies that the wrapping of text to the next line is disabled. NoWrap is implied
        //     when a point of origin is used instead of a layout rectangle. When drawing text
        //     within a rectangle, by default, text is broken at the last word boundary that
        //     is inside the rectangle's boundary and wrapped to the next line.
        NoWrap = 4096,
        //
        // Summary:
        //     Specifies that only entire lines are laid out in the layout rectangle. By default,
        //     layout continues until the end of the text or until no more lines are visible
        //     as a result of clipping, whichever comes first. The default settings allow the
        //     last line to be partially obscured by a layout rectangle that is not a whole
        //     multiple of the line height. To ensure that only whole lines are seen, set this
        //     flag and be careful to provide a layout rectangle at least as tall as the height
        //     of one line.
        LineLimit = 8192,
        //
        // Summary:
        //     Specifies that characters overhanging the layout rectangle and text extending
        //     outside the layout rectangle are allowed to show. By default, all overhanging
        //     characters and text that extends outside the layout rectangle are clipped. Any
        //     trailing spaces (spaces that are at the end of a line) that extend outside the
        //     layout rectangle are clipped. Therefore, the setting of this flag will have an
        //     effect on a string measurement if trailing spaces are being included in the measurement.
        //     If clipping is enabled, trailing spaces that extend outside the layout rectangle
        //     are not included in the measurement. If clipping is disabled, all trailing spaces
        //     are included in the measurement, regardless of whether they are outside the layout
        //     rectangle.
        NoClip = 16384
    }