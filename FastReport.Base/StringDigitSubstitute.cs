public enum StringDigitSubstitute
    {
        //
        // Summary:
        //     Specifies a user-defined substitution scheme.
        User = 0,
        //
        // Summary:
        //     Specifies to disable substitutions.
        None = 1,
        //
        // Summary:
        //     Specifies substitution digits that correspond with the official national language
        //     of the user's locale.
        National = 2,
        //
        // Summary:
        //     Specifies substitution digits that correspond with the user's native script or
        //     language, which may be different from the official national language of the user's
        //     locale.
        Traditional = 3
    }