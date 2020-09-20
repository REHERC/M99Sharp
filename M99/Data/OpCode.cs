namespace M99.Data
{
    public enum OpCode : short
    {
        VAL,
        RAW = VAL,
        STR,
        LDA,
        LDB,
        MOV,
        ADD,
        SUB,
        JMP,
        JPP,
        JEQ,
        JNE,
    }
}
