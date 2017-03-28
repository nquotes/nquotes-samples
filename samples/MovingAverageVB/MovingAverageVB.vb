Imports System
Imports Color = System.Drawing.Color
Imports NQuotes

Public Class MovingAverageVB
    Inherits MqlApi

    Const MAGICMA As Integer = 20050610

    Private MaximumRisk As Double = 0.02
    Private DecreaseFactor As Double = 3
    Private MovingPeriod As Integer = 12
    Private MovingShift As Integer = 6

    ' Calculate open positions
    Function CalculateCurrentOrders() As Integer
        Dim buys As Integer = 0, sells As Integer = 0

        For i As Integer = 0 To OrdersTotal() - 1
            If (Not OrderSelect(i, SELECT_BY_POS, MODE_TRADES)) Then Exit For
            If (OrderSymbol() = Symbol()) AndAlso (OrderMagicNumber() = MAGICMA) Then
                If (OrderType() = OP_BUY) Then buys += 1
                If (OrderType() = OP_SELL) Then sells += 1
            End If
        Next

        ' return orders volume
        If (buys > 0) Then Return (buys)
        Return (-sells)
    End Function

    ' Calculate optimal lot size
    Function LotsOptimized() As Double
        ' history orders total
        Dim orders As Integer = OrdersHistoryTotal()
        ' number of losses orders without a break
        Dim losses As Integer = 0
        ' select lot size
        Dim lot As Double = NormalizeDouble(AccountFreeMargin() * MaximumRisk / 1000.0, 1)
        ' calculate number of losses orders without a break
        If (DecreaseFactor > 0) Then
            For i As Integer = orders - 1 To 0 Step -1
                If (Not OrderSelect(i, SELECT_BY_POS, MODE_HISTORY)) Then
                    Print("Error in history!")
                    Exit For
                End If
                If (OrderSymbol() <> Symbol()) OrElse (OrderType() > OP_SELL) Then Continue For

                If (OrderProfit() > 0) Then Exit For
                If (OrderProfit() < 0) Then losses += 1
            Next
            If (losses > 1) Then lot = NormalizeDouble(lot - lot * losses / DecreaseFactor, 1)
        End If
        ' return lot size
        If (lot < 0.1) Then lot = 0.1
        Return (lot)
    End Function

    ' Check for open order conditions
    Sub CheckForOpen(symbol As String)
        ' go trading only for first tiks of new bar
        If (Volume(0) > 1) Then Return

        ' get Moving Average 
        Dim ma As Double = iMA(symbol, 0, MovingPeriod, MovingShift, MODE_SMA, PRICE_CLOSE, 0)

        ' sell conditions
        If (Open(1) > ma) AndAlso (Close(1) < ma) Then
            OrderSend(symbol, OP_SELL, LotsOptimized(), Bid, 3, 0, 0, "", MAGICMA, DateTime.MinValue, Color.Red)
            Return
        End If
        ' buy conditions
        If (Open(1) < ma) AndAlso (Close(1) > ma) Then
            OrderSend(symbol, OP_BUY, LotsOptimized(), Ask, 3, 0, 0, "", MAGICMA, DateTime.MinValue, Color.Blue)
        End If
    End Sub

    ' Check for close order conditions
    Sub CheckForClose(symbol As String)
        ' go trading only for first tiks of new bar
        If (Volume(0) > 1) Then Return
        ' get Moving Average 
        Dim ma As Double = iMA(symbol, 0, MovingPeriod, MovingShift, MODE_SMA, PRICE_CLOSE, 0)

        For i As Integer = 0 To OrdersTotal() - 1
            If (Not OrderSelect(i, SELECT_BY_POS, MODE_TRADES)) Then Exit For
            If (OrderMagicNumber() <> MAGICMA) OrElse (OrderSymbol() <> symbol) Then Continue For
            ' check order type 
            If (OrderType() = OP_BUY) Then
                If (Open(1) > ma) AndAlso (Close(1) < ma) Then OrderClose(OrderTicket(), OrderLots(), Bid, 3, Color.White)
                Exit For
            End If
            If (OrderType() = OP_SELL) Then
                If (Open(1) < ma) AndAlso (Close(1) > ma) Then OrderClose(OrderTicket(), OrderLots(), Ask, 3, Color.White)
                Exit For
            End If
        Next
    End Sub

    ' Start function
    Public Overrides Function start() As Integer
        ' check for history and trading
        If (Bars < 100) OrElse (Not IsTradeAllowed()) Then Return 0

        ' calculate open orders by current symbol
        Dim currentSymbol As String = Symbol()
        If (CalculateCurrentOrders() = 0) Then
            CheckForOpen(currentSymbol)
        Else
            CheckForClose(currentSymbol)
        End If

        Return 0
    End Function
End Class
