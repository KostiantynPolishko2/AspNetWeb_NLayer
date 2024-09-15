import { FC } from "react"
import { Display } from "../styles/General.styled";
import { TitleWrapper, BtnWrapper } from "../styles/ProductItem.styled";
import ProductItem from "../ProductItem/ProductItem";

interface IProductItemRecord{
    _width?: number,
    _nameProduct: string | null,
    _handleBtnSetUp: (e: React.FormEvent<HTMLElement>) => void,
}

const ProductItemRecord: FC<IProductItemRecord> = (props) => {

    if(props._nameProduct == null) return;

    return(
        <div style={{width: `${props._width || 500}px` }}>
            <Display _justify='none'>
                <TitleWrapper style={{width:'450px', textAlign: 'center'}}>SUBJECT DETAILS</TitleWrapper>
                <BtnWrapper onClick={props._handleBtnSetUp}>OPTIONS</BtnWrapper>
            </Display>
            <ProductItem nameProduct={props._nameProduct}/>
        </div>
    );
}

export default ProductItemRecord;