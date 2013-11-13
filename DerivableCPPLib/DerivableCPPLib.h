// DerivableCPPLib.h

#pragma once

using namespace System;

namespace DerivableCPPLib {

	
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

//--------------------------------------

Derivable f(double x, double a) {
	Derivable xd = Derivable::IndependendVariable(x);
	return a*xd*xd*xd - sin(xd/2);
}
	}

