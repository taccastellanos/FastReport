public enum StringTrimming
    {
        //
        // Summary:
        //     Specifies no trimming.
        None = 0,
        //
        // Summary:
        //     Specifies that the string is broken at the boundary of the last character that
        //     is inside the layout rectangle. This is the default.
        Character = 1,
        //
        // Summary:
        //     Specifies that the string is broken at the boundary of the last word that is
        //     inside the layout rectangle.
        Word = 2,
        //
        // Summary:
        //     Specifies that the string is broken at the boundary of the last character that
        //     is inside the layout rectangle and an ellipsis (...) is inserted after the character.
        EllipsisCharacter = 3,
        //
        // Summary:
        //     Specifies that the string is broken at the boundary of the last word that is
        //     inside the layout rectangle and an ellipsis (...) is inserted after the word.
        EllipsisWord = 4,
        //
        // Summary:
        //     Specifies that the center is removed from the string and replaced by an ellipsis.
        //     The algorithm keeps as much of the last portion of the string as possible.
        EllipsisPath = 5
    }