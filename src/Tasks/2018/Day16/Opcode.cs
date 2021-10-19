namespace App.Tasks.Year2018.Day16
{
    public enum Opcode
    {
        AddRegister,
        AddImmediate,
        MultiplyRegister,
        MultiplyImmediate,
        BitwiseAndRegister,
        BitwiseAndImmediate,
        BitwiseOrRegister,
        BitwiseOrImmediate,
        SetRegister,
        SetImmediate,
        GreaterThanImmediateRegister,
        GreaterThanRegisterImmediate,
        GreaterThanRegisterRegister,
        EqualImmediateRegister,
        EqualRegisterImmediate,
        EqualRegisterRegister
    }
}
