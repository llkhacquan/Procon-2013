#include <string.h>
extern "C"{
	#include <stdio.h>
	#include <stdlib.h>
	#include <string.h>
	static char *Smaz_cb[241] = {
	"\002s,\266","\003had\232\002leW","\003on_\216","","\001yS",
	"\002ma\255\002li\227","\003or_\260","","\002ll\230\003s_t\277",
	"\004fromg\002mel","","\003its\332","\001z\333","\003ingF","\001>\336",
	"\001_\000\003___(\002nc\344","\002nd=\003_on\312",
	"\002ne\213\003hat\276\003re_q","","\002ngT\003herz\004have\306\003s_o\225",
	"","\003ionk\003s_a\254\002ly\352","\003hisL\003_inN\003_be\252","",
	"\003_fo\325\003_of_\003_ha\311","","\002of\005",
	"\003_co\241\002no\267\003_ma\370","","","\003_cl\356\003enta\003_an7",
	"\002ns\300\001\"e","\003n_t\217\002ntP\003s,\205",
	"\002pe\320\003_we\351\002om\223","\002on\037","","\002y_G","\003_wa\271",
	"\003_re\321\002or*","","\002=\"\251\002ot\337","\003forD\002ou[",
	"\003_toR","\003_th\r","\003_it\366",
	"\003but\261\002ra\202\003_wi\363\002</\361","\003_wh\237","\002__4",
	"\003nd_?","\002re!","","\003ng_c","",
	"\003ly_\307\003ass\323\001a\004\002rir","","","","\002se_","\003of_\"",
	"\003div\364\002ros\003ere\240","","\002ta\310\001bZ\002si\324","",
	"\003and\a\002rs\335","\002rt\362","\002teE","\003ati\316","\002so\263",
	"\002th\021","\002tiJ\001c\034\003allp","\003ate\345","\002ss\246",
	"\002stM","","\002><\346","\002to\024","\003arew","\001d\030",
	"\002tr\303","","\001\n1\003_a_\222","\003f_tv\002veo","\002un\340","",
	"\003e_o\242","\002a_\243\002wa\326\001e\002","\002ur\226\003e_a\274",
	"\002us\244\003\n\r\n\247","\002ut\304\003e_c\373","\002we\221","","",
	"\002wh\302","\001f,","","","","\003d_t\206","","","\003th_\343",
	"\001g;","","","\001\r9\003e_s\265","\003e_t\234","","\003to_Y",
	"\003e\r\n\236","\002d_\036\001h\022","","\001,Q","\002_a\031","\002_b^",
	"\002\r\n\025\002_cI","\002_d\245","\002_e\253","\002_fh\001i\b\002e_\v",
	"","\002_hU\001-\314","\002_i8","","","\002_l\315","\002_m{",
	"\002f_:\002_n\354","\002_o\035","\002_p}\001.n\003\r\n\r\250","",
	"\002_r\275","\002_s>","\002_t\016","","\002g_\235\005which+\003whi\367",
	"\002_w5","\001/\305","\003as_\214","\003at_\207","","\003who\331","",
	"\001l\026\002h_\212","","\002,$","","\004withV","","","","\001m-","",
	"","\002ac\357","\002ad\350","\003TheH","","","\004this\233\001n\t",
	"","\002._y","","\002alX\003e,\365","\003tio\215\002be\\",
	"\002an\032\003ver\347","","\004that0\003tha\313\001o\006","\003was2",
	"\002arO","\002as.","\002at'\003the\001\004they\200\005there\322\005theird",
	"\002ce\210","\004were]","","\002ch\231\002l_\264\001p<","","",
	"\003one\256","","\003he_\023\002dej","\003ter\270","\002cou","",
	"\002by\177\002di\201\002eax","","\002ec\327","\002edB","\002ee\353","",
	"","\001r\f\002n_)","","","","\002el\262","","\003in_i\002en3","",
	"\002o_`\001s\n","","\002er\033","\003is_t\002es6","","\002ge\371",
	"\004.com\375","\002fo\334\003our\330","\003ch_\301\001t\003","\002hab","",
	"\003men\374","","\002he\020","","","\001u&","\002hif","",
	"\003not\204\002ic\203","\003ed_@\002id\355","","","\002ho\273",
	"\002r_K\001vm","","","","\003t_t\257\002il\360","\002im\342",
	"\003en_\317\002in\017","\002io\220","\002s_\027\001wA","","\003er_|",
	"\003es_~\002is%","\002it/","","\002iv\272","",
	"\002t_#\ahttp://C\001x\372","\002la\211","\001<\341","\003,a\224"
	};

	/* Reverse compression codebook, used for decompression */
	static char *Smaz_rcb[254] = {
	"_","the","e","t","a","of","o","and","i","n","s","e_","r","_th",
	"_t","in","he","th","h","he_","to","\r\n","l","s_","d","_a","an",
	"er","c","_o","d_","on","_of","re","of_","t_",",","is","u","at",
	"___","n_","or","which","f","m","as","it","that","\n","was","en",
	"_","_w","es","_an","_i","\r","f_","g","p","nd","_s","nd_","ed_",
	"w","ed","http://","for","te","ing","y_","The","_c","ti","r_","his",
	"st","_in","ar","nt",",","_to","y","ng","_h","with","le","al","to_",
	"b","ou","be","were","_b","se","o_","ent","ha","ng_","their","\"",
	"hi","from","_f","in_","de","ion","me","v",".","ve","all","re_",
	"ri","ro","is_","co","f_t","are","ea","._","her","_m","er_","_p",
	"es_","by","they","di","ra","ic","not","s,","d_t","at_","ce","la",
	"h_","ne","as_","tio","on_","n_t","io","we","_a_","om",",a","s_o",
	"ur","li","ll","ch","had","this","e_t","g_","e\r\n","_wh","ere",
	"_co","e_o","a_","us","_d","ss","\n\r\n","\r\n\r","=\"","_be","_e",
	"s_a","ma","one","t_t","or_","but","el","so","l_","e_s","s,","no",
	"ter","_wa","iv","ho","e_a","_r","hat","s_t","ns","ch_","wh","tr",
	"ut","/","have","ly_","ta","_ha","_on","tha","-","_l","ati","en_",
	"pe","_re","there","ass","si","_fo","wa","ec","our","who","its","z",
	"fo","rs",">","ot","un","<","im","th_","nc","ate","><","ver","ad",
	"_we","ly","ee","_n","id","_cl","ac","il","</","rt","_wi","div",
	"e,","_it","whi","_ma","ge","x","e_c","men",".com"
	};

	__declspec(dllexport) int smaz_compress(char *in, int inlen, char *out, int outlen) {

		unsigned int h1,h2,h3=0;
		int verblen = 0, _outlen = outlen;
		char verb[256], *_out = out;

		while(inlen) {
			int j = 7, needed;
			char *flush = NULL;
			char *slot;

			h1 = h2 = in[0]<<3;
			if (inlen > 1) h2 += in[1];
			if (inlen > 2) h3 = h2^in[2];
			if (j > inlen) j = inlen;

			/* Try to lookup substrings into the hash table, starting from the
			 * longer to the shorter substrings */
			for (; j > 0; j--) {
				switch(j) {
				case 1: slot = Smaz_cb[h1%241]; break;
				case 2: slot = Smaz_cb[h2%241]; break;
				default: slot = Smaz_cb[h3%241]; break;
				}
				while(slot[0]) {
					if (slot[0] == j && memcmp(slot+1,in,j) == 0) {
						/* Match found in the hash table,
						 * prepare a verbatim bytes flush if needed */
						if (verblen) {
							needed = (verblen == 1) ? 2 : 2+verblen;
							flush = out;
							out += needed;
							outlen -= needed;
						}
						/* Emit the byte */
						if (outlen <= 0) return _outlen+1;
						out[0] = slot[slot[0]+1];
						out++;
						outlen--;
						inlen -= j;
						in += j;
						goto out;
					} else {
						slot += slot[0]+2;
					}
				}
			}
			/* Match not found - add the byte to the verbatim buffer */
			verb[verblen] = in[0];
			verblen++;
			inlen--;
			in++;
	out:
			/* Prepare a flush if we reached the flush length limit, and there
			 * is not already a pending flush operation. */
			if (!flush && (verblen == 256 || (verblen > 0 && inlen == 0))) {
				needed = (verblen == 1) ? 2 : 2+verblen;
				flush = out;
				out += needed;
				outlen -= needed;
				if (outlen < 0) return _outlen+1;
			}
			/* Perform a verbatim flush if needed */
			if (flush) {
				if (verblen == 1) {
					flush[0] = (signed char)254;
					flush[1] = verb[0];
				} else {
					flush[0] = (signed char)255;
					flush[1] = (signed char)(verblen-1);
					memcpy(flush+2,verb,verblen);
				}
				flush = NULL;
				verblen = 0;
			}
		}
		return out-_out;
	}

	__declspec(dllexport) int smaz_decompress(char *in, int inlen, char *out, int outlen) {
		unsigned char *c = (unsigned char*) in;
		char *_out = out;
		int _outlen = outlen;
		//int realoutlen;

		while(inlen) {
			if (*c == 254) {
				/* Verbatim byte */
				if (outlen < 1) return _outlen+1;
				*out = *(c+1);
				out++;
				outlen--;
				c += 2;
				inlen -= 2;
			} else if (*c == 255) {
				/* Verbatim string */
				int len = (*(c+1))+1;
				if (outlen < len) return _outlen+1;
				memcpy(out,c+2,len);
				out += len;
				outlen -= len;
				c += 2+len;
				inlen -= 2+len;
			} else {
				/* Codebook entry */
				char *s = Smaz_rcb[*c];
				int len = strlen(s);

				if (outlen < len) return _outlen+1;
				memcpy(out,s,len);
				out += len;
				outlen -= len;
				c++;
				inlen--;
			}
		}
		return out-_out;
	}
	__declspec(dllexport) int Compress (char *in, int inlen, char *out, int outlen){
		int comlen = smaz_compress(in,inlen,out,outlen);
			FILE* f;
		fopen_s(&f,"a.bin","wb");
		
		fseek(f,0,0);

		//int i;
		//for(i=0;i<comprlen;i++){
			fwrite(out,sizeof(char),comlen,f);
		//}
		fclose(f);
		return comlen;
	}
	__declspec(dllexport) int Uncompress(char* out, int outlen){
		FILE* f;
		
		long lSize;
		char * buffer;
		size_t result;
		//char d[4096];
		fopen_s (&f, "b.bin" , "rb" );
		//get FILE length
		fseek (f , 0 , 2);
  lSize = ftell (f);
  rewind (f);
  buffer = (char*) malloc (sizeof(char)*lSize);
  result = fread (buffer,1,lSize,f);
fclose (f);
int decomprlen = smaz_decompress(buffer,lSize,out,outlen);
	return decomprlen;
	}
}
