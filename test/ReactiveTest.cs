namespace ERSTest
{
    using EtherReactorSimulator;
    using Xunit;

    public class ReactiveTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(12.0f, 0.If(2).Then(4).Else(12.0f).Value<float>());
            Assert.Equal(4, 0.If(0).Then(4).Else(12.0f).Value<float>());
            Assert.NotEqual(7, 0.If(0).Then(4).Else(15.0f).Value<float>());
            Assert.Equal(4m, 0.If(0).Then(4m).Else(11.0f).Value<decimal>());
        }
    }
}