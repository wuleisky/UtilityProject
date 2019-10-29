//   xmlns:ei="http://schemas.microsoft.com/expression/2010/interactions"
//             xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity" 
//<i:Interaction.Triggers>
//                    <ei:DataTrigger Binding = "{Binding Path=SelectedSlideItem,Converter={StaticResource IsNullOrEmptyConverter}}" Value="True">
//                        <ei:ChangePropertyAction PropertyName = "Visibility" Value="{x:Static Visibility.Collapsed}"/>
//                    </ei:DataTrigger>
//                    <ei:DataTrigger Binding = "{Binding Path=SelectedSlideItem,Converter={StaticResource TypeConverter}}" Value="{x:Type app:VideoSlideItem}">
//                        <ei:ChangePropertyAction PropertyName = "Visibility" Value="{x:Static Visibility.Collapsed}"/>
//                    </ei:DataTrigger>
//                    <ei:DataTrigger Binding = "{Binding Path=SelectedSlideItem,Converter={StaticResource TypeConverter}}" Value="{x:Type app:ShapeSlideItem}">
//                        <ei:ChangePropertyAction PropertyName = "Visibility" Value="{x:Static Visibility.Visible}"/>
//                        <ei:ChangePropertyAction PropertyName = "IsSelected" Value="True"/>
//                    </ei:DataTrigger>
//                    <ei:DataTrigger Binding = "{Binding Path=SelectedSlideItem,Converter={StaticResource TypeConverter}}" Value="{x:Type app:RichTextSlideItem}">
//                        <ei:ChangePropertyAction PropertyName = "Visibility" Value="{x:Static Visibility.Visible}"/>
//                        <ei:ChangePropertyAction PropertyName = "IsSelected" Value="True"/>
//                    </ei:DataTrigger>
//                    <ei:DataTrigger Binding = "{Binding Path=SelectedSlideItem,Converter={StaticResource TypeConverter}}" Value="{x:Type app:ImageSlideItem}">
//                        <ei:ChangePropertyAction PropertyName = "Visibility" Value="{x:Static Visibility.Visible}"/>
//                        <ei:ChangePropertyAction PropertyName = "IsSelected" Value="True"/>
//                    </ei:DataTrigger>
//                    <ei:DataTrigger Binding = "{Binding Path=SelectedSlideItem,Converter={StaticResource TypeConverter}}" Value="{x:Type app:TableSlideItem}">
//                        <ei:ChangePropertyAction PropertyName = "Visibility" Value="{x:Static Visibility.Collapsed}"/>
//                    </ei:DataTrigger>
//                </i:Interaction.Triggers>


//<i:Interaction.Triggers>
//                                                        <i:EventTrigger EventName = "{Binding Name, Source={x:Static Selector.SelectionChangedEvent}}" >
//                                                            < i:InvokeCommandAction Command = "{Binding StrokeDashArrayCommand}" CommandParameter="{Binding SelectedValue, ElementName=StrokeComboBox}"/>
//                                                        </i:EventTrigger>
//                                                    </i:Interaction.Triggers>

// <i:Interaction.Triggers>
//        <i:EventTrigger EventName = "{Binding Source={x:Static FrameworkElement.LoadedEvent},Path=Name}" >
//            < i:InvokeCommandAction Command = "{Binding Path=InitializationCommand}" />

//         </ i:EventTrigger>
//    </i:Interaction.Triggers>


//  <i:Interaction.Triggers>
//                <i:EventTrigger EventName = "{Binding Source={x:Static FrameworkElement.LoadedEvent},Path=Name}" >
//                    < me:ScreenshotAction Screenshot = "{Binding Path=Cover,Mode=OneWayToSource}" />

//                 </ i:EventTrigger>
//            </i:Interaction.Triggers>

//  <i:Interaction.Behaviors>
//                <me:ShortcutKeyBehavior Modifiers = "Control" Key="S" Command="{Binding Path=SaveCommand}"/>
//            </i:Interaction.Behaviors>


// <i:Interaction.Behaviors>
//                        <app:AssociatedObjectBehavior Binding = "{Binding Path=EditElement,Mode=OneWayToSource}" />
//                    </ i:Interaction.Behaviors>


//i:Interaction.Triggers>
//                            <i:EventTrigger EventName = "Click" >
//                                < i:Interaction.Behaviors>
//                                    <ei:ConditionBehavior>
//                                        <ei:ConditionalExpression>
//                                            <ei:ComparisonCondition LeftOperand = "{Binding Path=IsEnshrine}" RightOperand="False"/>
//                                        </ei:ConditionalExpression>
//                                    </ei:ConditionBehavior>
//                                </i:Interaction.Behaviors>
//                                <i:InvokeCommandAction Command = "{x:Static app:LocalMessage.Enshrine}"  CommandParameter="{Binding}"/>
//                            </i:EventTrigger>
//                            <i:EventTrigger EventName = "Click" >
//                                < i:Interaction.Behaviors>
//                                    <ei:ConditionBehavior>
//                                        <ei:ConditionalExpression>
//                                            <ei:ComparisonCondition LeftOperand = "{Binding Path=IsEnshrine}" RightOperand="True"/>
//                                        </ei:ConditionalExpression>
//                                    </ei:ConditionBehavior>
//                                </i:Interaction.Behaviors>
//                                <!--<ei:ChangePropertyAction PropertyName = "Background" Value="Gray"/>-->
//                                <ei:ChangePropertyAction TargetName = "UnenshrinePopup" PropertyName="IsOpen" Value="True"/>
//                            </i:EventTrigger>
//                        </i:Interaction.Triggers>


//    <i:Interaction.Triggers>
//                        <i:EventTrigger>
//                            <i:EventTrigger.EventName>
//                                <Binding Source = "{x:Static FrameworkElement.MouseLeftButtonDownEvent}" Path="Name"/>
//                            </i:EventTrigger.EventName>
//                            <ei:ChangePropertyAction PropertyName = "SelectedIndex" Value="-1"/>
//                        </i:EventTrigger>
//                        <i:EventTrigger EventName = "{Binding Name, Source={x:Static UIElement.MouseLeftButtonDownEvent}}" >
//                            < i:Interaction.Behaviors>
//                                <ei:ConditionBehavior>
//                                    <ei:ConditionalExpression>
//                                        <ei:ComparisonCondition LeftOperand = "{Binding Path=IsAddRichText}" RightOperand="True"/>
//                                    </ei:ConditionalExpression>
//                                </ei:ConditionBehavior>
//                            </i:Interaction.Behaviors>
//                            <me:InvokeEventCommandAction Command = "{x:Static app:LocalMessage.ItemAdd}" />
//                        </ i:EventTrigger>
//                        <i:EventTrigger EventName = "{Binding Name, Source={x:Static UIElement.MouseLeftButtonDownEvent}}" >
//                            < i:Interaction.Behaviors>
//                                <ei:ConditionBehavior>
//                                    <ei:ConditionalExpression>
//                                        <ei:ComparisonCondition LeftOperand = "{Binding Path=SelectedSlide}" Operator="NotEqual" RightOperand="{x:Null}"/>
//                                    </ei:ConditionalExpression>
//                                </ei:ConditionBehavior>
//                            </i:Interaction.Behaviors>
//                            <me:InvokeEventCommandAction Command = "{x:Static app:LocalMessage.ItemAdd}" />
//                        </ i:EventTrigger>
//                    </i:Interaction.Triggers>


//<i:Interaction.Behaviors>
//        <me:LoadedCommandBehavior Command = "{Binding Path=InitializationCommand}" />
//    </ i:Interaction.Behaviors>


//public class LoadedCommandBehavior : Behavior<FrameworkElement>
//{
//    static readonly Type OwnerType = typeof(LoadedCommandBehavior);

//    #region Command

//    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), OwnerType, new PropertyMetadata(default(ICommand)));

//    public ICommand Command
//    {
//        get { return (ICommand)GetValue(CommandProperty); }
//        set { SetValue(CommandProperty, value); }
//    }

//    #endregion

//    #region CommandParameter

//    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), OwnerType, new PropertyMetadata(default(object)));

//    public object CommandParameter
//    {
//        get { return (object)GetValue(CommandParameterProperty); }
//        set { SetValue(CommandParameterProperty, value); }
//    }

//    #endregion

//    protected override void OnAttached()
//    {
//        base.OnAttached();
//        AssociatedObject.Loaded += AssociatedObject_Loaded;
//    }

//    protected override void OnDetaching()
//    {
//        base.OnDetaching();
//        AssociatedObject.Loaded -= AssociatedObject_Loaded;
//    }

//    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
//    {
//        Command?.Execute(CommandParameter);
//    }
//}


//   <i:Interaction.Triggers>
//                                        <i:EventTrigger EventName = "{Binding Source={x:Static FrameworkElement.MouseLeftButtonDownEvent},Path=Name}" >
//                                            < i:Interaction.Behaviors>
//                                                <ei:ConditionBehavior>
//                                                    <ei:ConditionalExpression>
//                                                        <ei:ComparisonCondition Operator = "Equal" RightOperand="True">
//                                                            <ei:ComparisonCondition.LeftOperand>
//                                                                <Binding RelativeSource = "{RelativeSource FindAncestor, AncestorType={x:Type TabItem}}" Path="IsSelected"/>
//                                                            </ei:ComparisonCondition.LeftOperand>
//                                                        </ei:ComparisonCondition>
//                                                    </ei:ConditionalExpression>
//                                                </ei:ConditionBehavior>
//                                            </i:Interaction.Behaviors>
//                                            <ei:ChangePropertyAction TargetName = "ToggleButton" PropertyName="IsChecked" Value="{Binding ElementName=ToggleButton,Path=IsChecked,Converter={StaticResource AntonymyConverter}}"></ei:ChangePropertyAction>
//                                        </i:EventTrigger>
//                                    </i:Interaction.Triggers>

///// <summary>
///// 默认对当前元素进行截屏操作
///// </summary>
//public class ScreenshotAction : TriggerAction<FrameworkElement>
//{
//    public static readonly DependencyProperty ScreenshotProperty = DependencyProperty.Register("Screenshot", typeof(ImageSource), typeof(ScreenshotAction), new PropertyMetadata(default(ImageSource), PropertyChangedCallback));

//    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
//    {

//    }

//    public ImageSource Screenshot
//    {
//        get
//        {
//            return (ImageSource)GetValue(ScreenshotProperty);
//        }
//        set
//        {
//            SetValue(ScreenshotProperty, value);
//        }
//    }

//    protected override void Invoke(object parameter)
//    {
//        try
//        {
//            var width = (int)AssociatedObject.ActualWidth;
//            var height = (int)AssociatedObject.ActualHeight;
//            var bmp = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
//            bmp.Render(AssociatedObject);
//            Screenshot = bmp;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//}