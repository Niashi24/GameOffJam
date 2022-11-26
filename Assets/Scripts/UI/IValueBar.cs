public interface IValueBar
{
    float MaxValue { get; }
    float Value { get; }
    float Percent { get; }

    void SetMaxValue(float value);
    void SetValue(float value);
}
