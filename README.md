#Феникс#
======
##Oпис на проектот##
Во овој проект беше имплементирана едноставна игра во која летечки објект (чинија со кикирики) управуван од играчот треба да избегнува и уништува противнички летечки објекти (кригли со пиво) со цел да собере што е можно повеќе поени. Играта има едно ниво кое прогресивно се отежнува како што изминува времето. Со текот на времето, противниците стануваат посилни и прават поголема штета на играчот доколку се судрат со него. Играчот има оружје (кикирики) со кое може да ги уништува противниците. Во текот на играта на определен временски интервал кој е случајно определен, се појавуваат тегли со путер од кикирики кои му ја зголемуваат огнената моќ на играчот. Треба да се внимава бидејќи во случај на судир со противник, играчот губи определен дел од својот капацитет (енергија) и неговата огнена моќ се намалува дупло, но не помалку од стартната огнена моќ (5 единици). Кога играчот ќе ја истроши енергијата играта завршува.

##Потребен софтвер##
За да може да се менува кодот и да се игра играта потребно е да има инсталирано Visual Studio 2010 Express и Microsoft XNA Game Studio 4.0 за Visual Studio 2010.

##Имплементација##
Играта е имплементирана во .NET со користење на Microsoft XNA Game Studio 4.0. XNA овозможува едноставно вметнување на графички и звучни ресурси во врската преку готов pipeline за нивно компајлирање. Исто така XNA овозможува готов имплементиран циклус на игра кој соджи неколку главни методи кои се имплементирани во главната класа на играта GameLevel:

1. Иницијализација
Иницијализацијата се врши во методот:
<code> protected override void Initialize() </code>

2. Ажурирање
Aжурирањето се врши во методот:
<code> protected override void Update(GameTime gameTime) </code>

3. Цртање
Цртање се врши со методот:
<code>  protected override void Draw(GameTime gameTime) </code>

Играта содржи повеќе елементи кои се исцртуваат како Sprites објекти од самиот XNA. Овие објекти содржат текстури кои се додадени како ресурси во pipeline-от за ресурси. Секој објект има дефиниран правоаголник со координати во играта и соодветни димензии. Цртањето на секоја текстура се извршува во овие правоаголници. 
Објектите кои се цртаат се поделени на две групи, објекти кои прават колизија и објекти кои не прават колизија.
Објект кој не прави колизија е позадината имплеменирана во Background.cs. 
Објекти кои прават колизија се имплементирани во главната класа Colidable.cs од која наследуваат сите класи во играта: Player, Enemy, PowerUp, Bullet. 
За сите класи и имплеменираните методи во нив постојат детални коментари во кодот кои ги опишуваат.

Дополнително во играта се вметнати и звучни ефекти од класата SoundEffect кои се вчитуваат преку pipeline-от за ресурси и се пуштаат во соодветни моменти од играта.

##Графички ресурси##
Во прилог се дадени неколку слики од играта и од нејзините ресурси:

Куршум:
https://github.com/ena15/fenix/blob/master/graphics/Bullet.png

Непријател:
https://github.com/ena15/fenix/blob/master/graphics/mug.png

Играч:
https://github.com/ena15/fenix/blob/master/graphics/bowlOfpeanuts.png

PowerUp:
https://github.com/ena15/fenix/blob/master/graphics/peanutbutter.png

ScreenShots:
https://github.com/ena15/fenix/blob/master/graphics/screenShot1.PNG

https://github.com/ena15/fenix/blob/master/graphics/screenShot2.PNG