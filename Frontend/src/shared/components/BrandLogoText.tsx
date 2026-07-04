import palavraMonica from "../../assets/palavra_Monica.webp";

type BrandLogoTextProps = {
  size?: "hero" | "header" | "login";
  className?: string;
};

const sizeClass = {
  hero: "brand-logo-text--hero",
  header: "brand-logo-text--header",
  login: "brand-logo-text--login",
};

export function BrandLogoText({ size = "hero", className = "" }: BrandLogoTextProps) {
  return (
    <span className={`brand-logo-text ${sizeClass[size]} ${className}`} aria-label={"Escola Infantil Turma da M\u00f4nica"}>
      <span className="brand-logo-text__prefix">Escola Infantil Turma da</span>
      <img className="brand-logo-text__monica-image" src={palavraMonica} alt="" aria-hidden="true" />
    </span>
  );
}
