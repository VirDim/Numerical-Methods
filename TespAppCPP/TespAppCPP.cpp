// TespAppCPP.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"


#include <iostream>
#include <cmath>

class Derivable {
	double val, deriv;

public:
	Derivable(double _val, double _deriv) : val(_val), deriv(_deriv) {}
	Derivable(double c): val(c), deriv(0) {}
	static Derivable IndependendVariable(double x) { return Derivable(x,1); }

	double getValue() const {return val;}
	double getDerivative() const {return deriv;}

	friend Derivable operator+(const Derivable& f1, const Derivable& f2);
	friend Derivable operator-(const Derivable& f1, const Derivable& f2);
	friend Derivable operator*(const Derivable& f1, const Derivable& f2);
	friend Derivable operator/(const Derivable& f1, const Derivable& f2);
	friend Derivable cos(Derivable f);
	friend Derivable sin(Derivable f);
	friend Derivable ln(Derivable f);
};

Derivable operator+(const Derivable& f1, const Derivable& f2) {
	return Derivable(f1.val + f2.val, f1.deriv + f2.deriv);
}

Derivable operator-(const Derivable& f1, const Derivable& f2) {
	return Derivable(f1.val - f2.val, f1.deriv - f2.deriv);
}

Derivable operator*(const Derivable& f1, const Derivable& f2) {
	return Derivable(f1.val * f2.val, f1.deriv * f2.val + f1.val * f2.deriv);
}

Derivable operator/(const Derivable& f1, const Derivable& f2) {
	return Derivable(f1.val / f2.val, (f1.deriv * f2.val - f1.val * f2.deriv) / f2.val / f2.val);
}

Derivable cos(Derivable f) {
	return Derivable(cos(f.val), -sin(f.val)*f.deriv);
}

Derivable sin(Derivable f){
	return Derivable(sin(f.val),cos(f.val)*f.deriv);
}

//Derivable ln(Derivable f)
//{
//	return Derivable(ln(f.val),1/f.val);
//}

//--------------------------------------

Derivable fx(double x, double y) {
	Derivable xd = Derivable::IndependendVariable(x);
	Derivable yd = Derivable::IndependendVariable(y);
	//return cos(xd+0.5)+yd-0.8;
	//return sin(yd-1)+xd-1.3;
	//return sin(xd+1)-y -1.2;
	return sin(yd) +2*xd -2;
	
}

Derivable fy(double x,double y)
{
	Derivable xd = Derivable::IndependendVariable(x);
	Derivable yd = Derivable::IndependendVariable(y);
	//return 2*xd+cos(yd)-2;
	//return yd-sin(xd+1)-0.8;
	//return sin(yd)-2*xd-1.6;
	return cos(xd-1)+yd - 0.7;
}

int main() {
	static const double EPS = 1e-5;
	double x = 0,x0;
	double y = 0,y0;
	double a;
	//std::cin >> a;

	while (1) {
		Derivable thisF1 = fx(x,y);
		Derivable thisF2 = fy(x,y);
		if (fabs(thisF1.getValue()) < EPS && (fabs(thisF2.getValue())<EPS)) break;
		x = x - thisF1.getValue() / thisF1.getDerivative();
		y = y - thisF2.getValue() / thisF2.getDerivative();
		std::cout << "x: "<< x <<" y: " << y<<std::endl;
	}
	//std::cout << x << std::endl;
}