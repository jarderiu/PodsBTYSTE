   M S B A M L   `   `   `  ���� K  GPods_Score_Board, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     J FXamlAnimatedGif, Version=1.1.9.0, Culture=neutral, PublicKeyToken=null<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  !localclr-namespace:WpfApp1    � Title$  	Pods Game�� � Cursor$ Arrow�� � WindowState$ 	Maximized=�����  �� ImageSource$ Image/vsBackground_v3.png��.���  � RowDefinitions �� ��  � ColumnDefinitions �� �� .+��� -    ��Name$ p1Avatar��$��Image/SpriteA.png�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  �� -    ��Name$ p1Score��$
��Left=�$��22��$��15,0,0,6q�	 �� TextWrapping$
	 Wrap=�$��0��$��Bottom=�$	��100��
����  �
������ 
������ $3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�$
��30px(�
 �� 
IsReadOnly
 . �� -   $ p2Avatar��$��1��$��0��$��Image/SpriteB.png��$��0.5,0,0,1.5q�   �� -   $ p2Score��$��1��$��0��$
��Left=�$��22��$��30,0,0,6q�$
	 Wrap=�$��0��$��Bottom=�$	��100��
������ 
���  ��
������ $3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�$
��30px(�
 . � $��1��    �� ��  �� �� �� �� .+��� -    ��Name$ p1AvatarABorder��
���   �$��5q�$��0��$��0��._��� -   $ 	p1AvatarA��$��0��$��0��$��Image/SpriteA.png���� -   $ p1AvatarBBorder��
���   �$��0q�$��1��$��0��._��� -   $ 	p1AvatarB��$��1��$��0��$��Image/SpriteB.png���� -	   $ p1AvatarCBorder��
���   �$��0q�$��2��$��0��._��� -
   $ 	p1AvatarC��$��2��$��0��$��Image/SpriteC.png���� -   $ p1AvatarDBorder��
���   �$��0q�$��3��$��0��._��� -   $ 	p1AvatarD��$��3��$��0��$��Image/SpriteD.png���� -   $ p1AvatarEBorder��
���   �$��0q�$��0��$��1��._��� -   $ 	p1AvatarE��$��0��$��1��$��Image/SpriteE.png���� -   $ p1AvatarFBorder��
���   �$��0q�$��1��$��1��._��� -   $ 	p1AvatarF��$��1��$��2��$��Image/SpriteF.png���� -   $ p1AvatarGBorder��
���   �$��0q�$��2��$��1��._��� -   $ 	p1AvatarG��$��2��$��3��$��Image/SpriteG.png���� -   $ p1AvatarHBorder��
���   �$��0q�$��3��$��1��._��� -   $ 	p1AvatarH��$��3��$��4��$��Image/SpriteH.png��� $��1��$��1��$��0.5,0.5,0,-0.5q�    �� ��  �� �� �� �� .+��� -   $ p2AvatarABorder��
���   �$��0q�$��0��$��0��._��� -   $ 	p2AvatarA��$��0��$��0��$��Image/SpriteA.png���� -   $ p2AvatarBBorder��
���   �$��5q�$��1��$��0��._��� -   $ 	p2AvatarB��$��1��$��0��$��Image/SpriteB.png���� -   $ p2AvatarCBorder��
���   �$��0q�$��2��$��0��._��� -   $ 	p2AvatarC��$��2��$��0��$��Image/SpriteC.png���� -   $ p2AvatarDBorder��
���   �$��0q�$��3��$��0��._��� -   $ 	p2AvatarD��$��3��$��0��$��Image/SpriteD.png���� -   $ p2AvatarEBorder��
���   �$��0q�$��0��$��1��._��� -   $ 	p2AvatarE��$��0��$��1��$��Image/SpriteE.png���� -   $ p2AvatarFBorder��
���   �$��0q�$��1��$��1��._��� -    $ 	p2AvatarF��$��1��$��2��$��Image/SpriteF.png���� -!   $ p2AvatarGBorder��
���   �$��0q�$��2��$��1��._��� -"   $ 	p2AvatarG��$��2��$��3��$��Image/SpriteG.png���� -#   $ p2AvatarHBorder��
���   �$��0q�$��3��$��1��._��� -$   $ 	p2AvatarH��$��3��$��4��$��Image/SpriteH.png���� -%    ��Name$ btnStartGame��$��Strart Game��$��Right=�$��	0,10,10,0q�$	��Top=�$��74��$��1��$��20��$3��-Fonts/PressStart2P-Regular.ttf#Press Start 2P)�