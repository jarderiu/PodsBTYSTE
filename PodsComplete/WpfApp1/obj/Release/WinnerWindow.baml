   M S B A M L   `   `   `  ���� K  GPods_Score_Board, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.WinnerWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     J FXamlAnimatedGif, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  !localclr-namespace:WpfApp1    � WindowState$  	Maximized=� � Title$ Pods!��$
��1080��$
��1920��
���   �.��� ��Z�  � RowDefinitions �� $
��133* ��� $
��320* ��� $
��501* ��� $	��99* � � ColumnDefinitions �� $
��130* ��� $
��349* ��� $
��349* ��� $
��130* �.+��� -    ��Name$ image2��$
��Left=�$��78��$��10,10.5,0,0q�$��3��$	��Top=�$
��1894��$��4��$��Image/bricks.png��$
��Fill=��� $��0��$��0��
�������$��3q�$
��Left=�$	��Top=�$��86��$
��1895��$��4��$��	10,10,0,0q�._��  �� �� �� .+��� -   $ mePlayer��$��0,0,0,0q�'  !XamlAnimatedGif.AnimationBehavior   	SourceUri$ Image/pods.gif?� J� OpacityMask �	#FF442E2E$��0��$��0��$
��Left=��� $��Stretch=�
������ $��0,0,0,0q� �� TextWrapping$
 Wrap=� �� TextAlignment$ Center=�
�������$�� Pods!��$��Center=�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�$��Stretch=�$��1��$��60(�$
��Bold"�	 �� 
IsReadOnly	 . 
���   ��� -   $ 	mePlayer2��$��0,0,0,0q�$ Image/pods.gif?� �	#FF442E2E$��0��$��2��$��Right=�$��Center=�� $��0��$��1��$��4��$��10,10,10,0.5q� �� �� �� �� �� ��  �� $
��250* ��� $
��698* ��� $
��691* ��� $
��257* �.+��� -   
 ��Name$
 
RankingPos��$��Stretch=�$��Stretch=�$��0��$��0��$
��Rank��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�	 . $��30(�$ Center=�
���   �
��� �|�
��� �|�$��Center=�$��Center=��� -   $
 RankingPos2��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��1��$��Stretch=�$��0��$��1��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�	 . $��30(�$ Center=�
���   �$��50��
��� ���
��� ���$��Center=��� -   $
 RankingPos3��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��2��$��Stretch=�$��0��$��2��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�	 . $��30(�$ Center=�
���   �$��50��
��� ���
��� ���$��Center=��� -   $
 RankingPos4��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��3��$��Stretch=�$��0��$��3��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�	 . $��30(�$ Center=�
���   �$��50��
��� E��
��� E��$��Center=��� -   $
 RankingPos5��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��4��$��Stretch=�$��0��$��4��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�	 . $��30(�$ Center=�
���   �$��50��
������
������$��Center=��� -	   $
 RankingPos6��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��5��$��Stretch=�$��0��$��5��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�	 . $��30(�$ Center=�
���   �$��50��
������
������$��Center=��� -
   $
 NamePos��$��Stretch=�$��	0,0,690,0q�$
 Wrap=�$ Center=�$��Stretch=�$��1�� J� RenderTransformOrigin$ 0.5,0.5&�$��0��	 . $��30(�$	��600��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� �|�
��� �|�$��Center=�$��2������ *NameArray[0]+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� -   $
 NamePos2��$��Stretch=�$��	0,0,690,0q�$
 Wrap=�$��Stretch=�$��1��$ 0.5,0.5&�$��1��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� ���
��� ���$��Center=�$��2������ *NameArray[1]+$ TwoWay=�$ PropertyChanged=��� -   $
 NamePos3��$��Stretch=�$��	0,0,690,0q�$
 Wrap=�$��Stretch=�$��1��$ 0.5,0.5&�$��2��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� ���
��� ���$��Center=�$��2������ *NameArray[2]+$ TwoWay=�$ PropertyChanged=��� -   $
 NamePos4��$��Stretch=�$��	0,0,690,0q�$
 Wrap=�$��Stretch=�$��1��$ 0.5,0.5&�$��3��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� E��
��� E��$��Center=�$��2������ *NameArray[3]+$ TwoWay=�$ PropertyChanged=��� -   $
 NamePos5��$��Stretch=�$��	0,0,690,0q�$
 Wrap=�$��Stretch=�$��1��$ 0.5,0.5&�$��4��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
������
������$��Center=�$��2������ *NameArray[4]+$ TwoWay=�$ PropertyChanged=��� -   $
 NamePos6��$��Stretch=�$��	0,0,690,0q�$
 Wrap=�$��Stretch=�$��1��$ 0.5,0.5&�$��5��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
������
������$��Center=�$��2������ *NameArray[5]+$ TwoWay=�$ PropertyChanged=��� -   $
 	SchoolPos��$��Stretch=�$��	0.5,0,0,0q�$
 Wrap=�$��Center=�$��Stretch=�$��2��$��0��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� �|�
��� �|�$��Center=����� *SchoolArray[0]+$ TwoWay=�$ PropertyChanged=��� -   $
 
SchoolPos2��$��Stretch=�$��	0.5,0,0,0q�$
 Wrap=�$��Stretch=�$��2��$��1��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� ���
��� ���$��Center=����� *SchoolArray[1]+$ TwoWay=�$ PropertyChanged=��� -   $
 
SchoolPos3��$��Stretch=�$��	0.5,0,0,0q�$
 Wrap=�$��Stretch=�$��2��$��2��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� ���
��� ���$��Center=����� *SchoolArray[2]+$ TwoWay=�$ PropertyChanged=��� -   $
 
SchoolPos4��$��Stretch=�$��	0.5,0,0,0q�$
 Wrap=�$��Stretch=�$��2��$��3��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� E��
��� E��$��Center=����� *SchoolArray[3]+$ TwoWay=�$ PropertyChanged=��� -   $
 
SchoolPos5��$��Stretch=�$��	0.5,0,0,0q�$
 Wrap=�$��Stretch=�$��2��$��4��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
������
������$��Center=����� *SchoolArray[4]+$ TwoWay=�$ PropertyChanged=��� -   $
 
SchoolPos6��$��Stretch=�$��	0.5,0,0,0q�$
 Wrap=�$��Stretch=�$��2��$��5��	 . $��30(�$	��400��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
������
������$��Center=����� *SchoolArray[5]+$ TwoWay=�$ PropertyChanged=��� -   $
 RankingArray��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$ Center=�$��Stretch=�$��3��$ 0.5,0.5&�$��0��	 . $��40(�$	��150��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� �|�
��� �|�$��Center=����� *ScoreArray[0]+$ TwoWay=�$ PropertyChanged=��� -   $
 RankingArray2��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��Stretch=�$��3��$ 0.5,0.5&�$��1��	 . $��30(�$	��150��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� ���
��� ���$��Center=����� *ScoreArray[1]+$ TwoWay=�$ PropertyChanged=��� -   $
 RankingArray3��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��Stretch=�$��3��$ 0.5,0.5&�$��2��	 . $��30(�$	��150��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� ���
��� ���$��Center=����� *ScoreArray[2]+$ TwoWay=�$ PropertyChanged=��� -   $
 RankingArray4��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��Stretch=�$��3��$ 0.5,0.5&�$��3��	 . $��30(�$	��150��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
��� E��
��� E��$��Center=����� *ScoreArray[3]+$ TwoWay=�$ PropertyChanged=��� -   $
 RankingArray5��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��Stretch=�$��3��$ 0.5,0.5&�$��4��	 . $��30(�$	��150��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
������
������$��Center=����� *ScoreArray[4]+$ TwoWay=�$ PropertyChanged=��� -   $
 RankingArray6��$��Stretch=�$��0,0,0,0q�$
 Wrap=�$��Stretch=�$��3��$ 0.5,0.5&�$��5��	 . $��30(�$	��150��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�
���   �
������
������$��Center=����� *ScoreArray[5]+$ TwoWay=�$ PropertyChanged=�� $��10,9.5,9.5,0q�$��2��$��2��$	��482��$	��Top=� �� �� �� �� �� ��  �� $
��125* ��� $
��344* �.+��� $��Center=�$��Stretch=�$��0,0,0,0q�
���   �
�������
�������$��2q�$
 Wrap=�$��Name:��$��Stretch=�$��0��$��0��	 . $��30(�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� $��Center=�$��Stretch=�$��0,0,0,0q�
���   �
�������
�������$��2q�$
 Wrap=�$��School:��$��Stretch=�$��0��$��1��	 . $��30(�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� $��Center=�$��Stretch=�$��0,0,0,0q�
���   �
�������
�������$��2q�$
 Wrap=�$��Score:��$��Stretch=�$��0��$��2��	 . $��30(�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� -   $
 	NameField��$��Center=�$��Stretch=�$��0,0,0,0q�
���   �
�������
�������$��2q�$
 Wrap=�$��Stretch=�$��1��$��0��$��30(�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)����� *	NameOfKid+�� -   $
 SchoolField��$��Center=�$��Stretch=�$��0,0,0,0q�
���   �
�������
�������$��2q�$
 Wrap=�$��Stretch=�$��1��$��1��$��30(�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)����� *SchoolOfKid+�� -   $
 
ScoreField��$��Center=�$��Stretch=�$��0,0,0,0q�
���   �
�������
�������$��2q�$
 Wrap=�$��Stretch=�$��1��$��2��$��30(�$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)����� *
Scoreing+�� -    ��Name$ ScoreEnteryButton��$��Submit��$��Stretch=�$��0,10,0,0q�$��Stretch=�$��0��$��2��$��30(�$��4q�
��� �|�
����  �$��3��$��1��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� -    $ RestartButton��$��Reset��$��Stretch=�$��0,10,0,0q�$��Stretch=�$��0��$��2��$��30(�$��4q�
��� �|�
����  �$��4��$��1��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� -!   $ StartButton��$��Close��$��Stretch=�$��0,10,0,0q�$��Stretch=�$��0��$��2��$��30(�$��4q�
��� �|�
����  �$��5��$��1��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�� $��2��$
��Left=�$	��580��$��	10,10,0,0q�$��2��$	��Top=�$	��937��$��2��$��2�� �� $
��121* ��� $
��169* � �� $
��251* ��� $
��434* ��� $
��252* �.+��� -"   $ image��$
��Left=�$	��236��$��	10,10,0,0q�$	��Top=�$	��231��$��2���� -#   $ image1��$��2��$
��Left=�$	��236��$	��Top=�$	��242��$��	10,10,0,0q�$��2���� $��Stretch=�$��10,10,10,10q�$��Stretch=�$
��fill=�$��Image/winnerLogo.png�� �	#FF442E2E$��0��$��0���� $��Stretch=�$��10,10,10,10q�$��Stretch=�$
��Fill=�$��Image/winnerLogo.png�� �	#FF442E2E$��0��$��2��� $��1��$
��Left=�$	��236��$��	10,10,0,0q�$	��Top=�$	��414��$��2�� �� �� .+��� -$    ��Name$ Player1��$��Center=�$��10,10,10,10q�$��Center=�$��Stretch=�$��Stretch=�$��0��$��30(�$��4q�
������ 
��� ���$��0��
������ $3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� -%   $ player1Score��$��Center=�$��10,10,10,10q�$��Center=�$��Stretch=�$��Stretch=�$��0��$��30(�$��4q�
������ 
������ 
��� ���$��1��$��1��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)��� -&   $ winnerAvatarHolder��$��10,10,162,0q�$��Image/SpriteH.png�� �	#FF442E2E$��0��$��2��$��1���� $��
272,10,0,0q� �	#FF442E2E$��1��$��1��$��Image/castle.png��$
��Fill=�$��2��